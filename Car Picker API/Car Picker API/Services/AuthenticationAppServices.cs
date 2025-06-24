namespace Car_Picker_API.Services
{
    using CarPicker_API.Context;
    using global::Car_Picker_API.DTOs.Authentication;
    using global::Car_Picker_API.Entities;
    using global::Car_Picker_API.Helpers;
    using global::Car_Picker_API.Helpers.Enums;
    using global::Car_Picker_API.Interfaces;

    namespace Car_Picker_API.Servicess
    {
        public class AuthenticationAppServices : IUserAuthenticationInterface
        {
            private readonly CarPickerDbContext _context;
            public AuthenticationAppServices(CarPickerDbContext context)
            {
                _context = context;
            }
            public async Task<string> SignUp(SignUpDTO input)
            {
                if (!ValidationHelper.ISValidFullName(input.FullName) ||
                    !ValidationHelper.IsValidEmail(input.Email) ||
                    !ValidationHelper.IsValidDateOfBirth(input.DateOfBirth) ||
                    !ValidationHelper.IsValidPhoneNumber(input.PhoneNumber) ||
                    !ValidationHelper.IsValidPassword(input.Password))
                {

                    return "Invalid input data.";
                }


                if (_context.Users.Any(u => u.Email == input.Email))
                {
                    return "Email already exists.";
                }

                var drivingLicenseFrontPath = await SavingHelper.SaveFileToFolder(input.DrivingLicenseFrontImage, "CarLicenses");
                var drivingLicenseBackPath = await SavingHelper.SaveFileToFolder(input.DrivingLicenseBackImage, "CarLicenses");
                var nationalIDFrontPath = await SavingHelper.SaveFileToFolder(input.NationalIDFrontImage, "IdImages");
                var nationalIDBackPath = await SavingHelper.SaveFileToFolder(input.NationalIDBackImage, "IdImages");

                Console.WriteLine($"Driving License Front File: {input.DrivingLicenseFrontImage.FileName}");
                Console.WriteLine($"National ID Front File: {input.NationalIDFrontImage.FileName}");

                Console.WriteLine($"Saved Driving License Front Path: {drivingLicenseFrontPath}");
                Console.WriteLine($"Saved National ID Front Path: {nationalIDFrontPath}");


                User user = new User
                {
                    FullName = input.FullName,
                    Password = HashingHelper.HashValueWith384(input.Password),
                    PhoneNumber = input.PhoneNumber,
                    DateOfBirth = input.DateOfBirth,
                    Email = input.Email,
                    Gender = Enum.TryParse<Gender>(input.Gender, true, out var parsedGender)
                     ? parsedGender
                    : throw new ArgumentException("Invalid gender value"),
                    DrivingLicenseFrontImagePath = drivingLicenseFrontPath,
                    DrivingLicenseBackImagePath = drivingLicenseBackPath,
                    NationalIDFrontImagePath = nationalIDFrontPath,
                    NationalIDBackImagePath = nationalIDBackPath,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    CreationDate = DateTime.Now,
                    IsVerified = false,
                    IsActive = true,
                    RoleId = Helpers.Enums.Role.Client,
                    OTPCode = new Random().Next(11111, 99999).ToString(),
                    OTPExpiry = DateTime.Now.AddMinutes(5)

                };
                await MailingHelper.SendEmail(input.Email, user.OTPCode, "Sign Up  OTP", "Complete Sign Up Operation");

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return "Please verify your Email using the OTP sent.";
            }

            public async Task<string> Verification(VerificationDTO input)
            {
                if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.OTPCode))
                    return ("PhoneNumber and OTP code are required.");

                var user = _context.Users.Where(u => u.Email == input.Email && u.OTPCode == input.OTPCode
                && u.IsLoggedIn == false && u.OTPExpiry > DateTime.Now).SingleOrDefault();

                if (user == null)
                {
                    return "User not found";
                }
                if (input.IsSignup)
                {

                    user.IsVerified = true;
                    user.OTPExpiry = null;
                    user.OTPCode = null;
                    _context.Update(user);
                    _context.SaveChanges();
                    return "Your Account Is Verifyed";
                }
                else
                {
                    user.LastLoginTime = DateTime.Now;
                    user.IsLoggedIn = true;
                    user.OTPExpiry = null;
                    user.OTPCode = null;

                    _context.Update(user);
                    _context.SaveChanges();
                    var response = TokenHelper.GenerateJWTToken(user.Id.ToString(), "Client");
                    return response;

                }

            }

            public async Task<string> SignIn(SignInputDTO input)
            {
                if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
                    return ("Email and Password are required");

                var hashedPassword = HashingHelper.HashValueWith384(input.Password);

                var user = _context.Users
                    .Where(u => u.Email == input.Email && u.Password == hashedPassword && u.IsLoggedIn == false)
                    .SingleOrDefault();

                if (user == null)
                    return "User not found";

                var otp = new Random().Next(11111, 99999).ToString();
                user.OTPCode = otp;
                user.OTPExpiry = DateTime.Now.AddMinutes(5);

                await MailingHelper.SendEmail(input.Email, otp, "Sign In OTP", "Complete Sign In Operation");

                _context.Update(user);
                await _context.SaveChangesAsync();

                return "Check your email, OTP has been sent!";
            }

            public async Task<bool> SendOTP(string email)
            {
                var user = _context.Users.Where(u => u.Email == email && u.IsLoggedIn == false).SingleOrDefault();
                if (user == null)
                {
                    return false;
                }
                Random otp = new Random();
                user.OTPCode = otp.Next(11111, 99999).ToString();
                user.OTPExpiry = DateTime.Now.AddMinutes(3);
                await MailingHelper.SendEmail(email, user.OTPCode, "Reset Password OTP", "Complete Reset Password");

                _context.Update(user);
                _context.SaveChanges();

                return true;
            }

            public async Task<bool> ResetPassword(ResetPasswordDTO input)
            {
                if (input == null || string.IsNullOrWhiteSpace(input.Email)
                 || string.IsNullOrWhiteSpace(input.Password)
                 || string.IsNullOrWhiteSpace(input.OTPCode))

                {
                    return false;

                }

                if (!ValidationHelper.IsValidEmail(input.Email))
                {
                    return false;
                }

                var user = _context.Users.Where(u => u.Email == input.Email && u.OTPCode == input.OTPCode
                && u.IsLoggedIn == false && u.OTPExpiry > DateTime.Now).SingleOrDefault();

                if (user == null)
                {
                    return false;
                }
               
                user.Password = input.Password;
                user.OTPCode = null;
                user.OTPExpiry = null;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return true;
            }

            public async Task<bool> SignOut(int userId)
            {
                var user = _context.Users.Where(u => u.Id == userId && u.IsLoggedIn == true).SingleOrDefault();
                if (user == null)
                {
                    return false;
                }

                user.LastLoginTime = DateTime.Now;
                user.IsLoggedIn = false;

                _context.Update(user);
                _context.SaveChanges();

                return true;
            }



        }
    }

}
