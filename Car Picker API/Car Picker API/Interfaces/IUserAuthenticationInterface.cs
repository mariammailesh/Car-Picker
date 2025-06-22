using Car_Picker_API.DTOs.Authentication;

namespace Car_Picker_API.Interfaces
{
    public interface IUserAuthenticationInterface
    {
        Task<string> SignUp(SignUpDTO input);

        Task<string> Verification(VerificationDTO input);

        Task<string> SignIn(SignInputDTO input);
        Task<bool> SendOTP(string email);
        Task<bool> ResetPassword(ResetPasswordDTO input);

        Task<bool> SignOut(int userId);


    }
}
