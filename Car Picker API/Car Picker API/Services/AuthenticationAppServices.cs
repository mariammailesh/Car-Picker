namespace Car_Picker_API.Services
{
    using CarPicker_API.Context;
    using global::Car_Picker_API.DTOs;
    using global::Car_Picker_API.Entities;
    using global::Car_Picker_API.Helpers;
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

            public async Task<bool> SendOTP(string email)
            {
                var user = _context.Users.Where(u => u.Email == email && u.IsLoggedIn == false).SingleOrDefault();
                if (user == null)
                {
                    return false;
                }
                Random otp = new Random();
                user.OTPCode = otp.Next(11111, 99999).ToString();
                user.OTPExipry = DateTime.Now.AddMinutes(3);
                await MailingHelper.SendEmail(email, user.OTPCode, "Reset Password OTP", "Complete Reset Password");

                _context.Update(user);
                _context.SaveChanges();

                return true;
            }

            public async Task<string> SignIn(SignInputDTO input)
            {
                var originalUserName = input.Username;
                input.Username = HashingHelper.HashValueWith384(input.Username);
                var user = _context.Users.Where(u => (u.Email == input.Username || u.Username == input.Username) && u.Password == input.Password && u.IsLoggedIn == false).SingleOrDefault();
                if (user == null)
                {
                    return "User not found";
                }

                Random random = new Random();
                var otp = random.Next(11111, 99999);
                user.OTPCode = otp.ToString();

                user.OTPExipry = DateTime.Now.AddMinutes(5);
                await MailingHelper.SendEmail(originalUserName, user.OTPCode, "Sign In OTP", "Complete Sign In Operation");

                _context.Update(user);
                _context.SaveChanges();

                return "Check your emnail OTP has been sent!";
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

            public async Task<string> SignUp(SignUpDTO input)
            {
                if (!ValidationHelper.ISValidFullName(input.FullName) ||
                    !ValidationHelper.IsValidDateOfBirth(input.DateOfBirth) ||
                    !ValidationHelper.IsValidPhoneNumber(input.PhoneNumber) ||
                    !ValidationHelper.IsValidPassword(input.Password))
                {

                    return "Invalid input data.";
                }


                if (_context.Users.Any(u => u.PhoneNumber == input.PhoneNumber))
                {
                    return "Phone Number already exists.";
                }

                // Save images
                var carLicensePath = await SaveFileToFolder(input.DrivingLicenseFrontImagePath, "CarLicenses");
                var idImagePath = await SaveFileToFolder(input.NationalIDFrontImagePath, "IdImages");

                User user = new User
                {
                    FullName = input.FullName,
                    Password = HashingHelper.HashValueWith384(input.Password),
                    PhoneNumber = input.PhoneNumber,
                    DateOfBirth = input.DateOfBirth,
                    Gender = input.Gender,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    CreationDate = DateTime.Now,
                    IsVerified = false,
                    IsActive = true,
                    RoleId = 3,
                    OTPCode = new Random().Next(11111, 99999).ToString(),
                    OTPExpiry = DateTime.Now.AddMinutes(5),
                    DrivingLicenseFrontImagePath = input.DrivingLicenseFrontImagePath,
                    DrivingLicenseBackImagePath = input.DrivingLicenseBackImagePath,
                    NationalIDFrontImagePath = input.NationalIDFrontImagePath,
                    NationalIDBackImagePath = input.NationalIDBackImagePath,
                    
                };
                await MailingHelper.SendEmail(input.PhoneNumber, user.OTPCode, "Sign Up  OTP", "Complete Sign Up Operation");

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return "Please verify your Phone Number using the OTP sent.";
            }

            public async Task<string> Verification(VerificationDTO input)
            {
                if (string.IsNullOrWhiteSpace(input.PhoneNumber) || string.IsNullOrWhiteSpace(input.OTPCode))
                    return ("Email and OTP code are required.");

                var user = _context.Users.Where(u => u.PhoneNumber == input.PhoneNumber && u.Otpcode == input.OTPCode
                && u.IsLogedIn == false && u.Otpexpiry > DateTime.Now).SingleOrDefault();

                if (user == null)
                {
                    return "User not found";
                }
                if (input.IsSignup)
                {

                    user.IsVerified = true;
                    user.Otpexpiry = null;
                    user.Otpcode = null;
                    _context.Update(user);
                    _context.SaveChanges();
                    return "Your Account Is Verifyed";
                }
                else
                {
                    user.LastLoginTime = DateTime.Now;
                    user.IsLogedIn = true;
                    user.Otpexpiry = null;
                    user.Otpcode = null;

                    _context.Update(user);
                    _context.SaveChanges();
                    var response = TokenHelper.GenerateJWTToken(user.Id.ToString(), "Client");
                    return response;

                }

            }

            public async Task<bool> ResetPassword(ResetPasswordDTO input)
            {
                if (input == null || string.IsNullOrWhiteSpace(input.PhoneNumber)
             || string.IsNullOrWhiteSpace(input.Password)
             || string.IsNullOrWhiteSpace(input.ConfirmPassword)
             || string.IsNullOrWhiteSpace(input.OTPCode))

                {
                    return false;

                }

                if (!ValidationHelper.IsValidPhoneNumber(input.PhoneNumber))
                {
                    return false;
                }

                var user = _context.Users.Where(u => u.Email == input.PhoneNumber && u.Otpcode == input.OTPCode
                && u.IsLogedIn == false && u.Otpexpiry > DateTime.Now).SingleOrDefault();

                if (user == null)
                {
                    return false;
                }
                if (input.Password != input.ConfirmPassword)
                {
                    return false;
                }
                user.Password = input.ConfirmPassword;
                user.Otpcode = null;
                user.Otpexpiry = null;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return true;
            }
        }
    }

}
