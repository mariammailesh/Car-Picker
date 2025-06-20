namespace Car_Picker_API.Services
{
    using CarPicker_API.Context;
    using global::Car_Picker_API.DTOs;
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


        }
    }

}
