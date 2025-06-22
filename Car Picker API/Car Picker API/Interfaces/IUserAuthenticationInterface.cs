using Car_Picker_API.DTOs.Authentication;

namespace Car_Picker_API.Interfaces
{
    public interface IUserAuthenticationInterface
    {
        Task<string> SignIn(SignInputDTO input);
        Task<bool> SendOTP(string email);

        Task<bool> SignOut(int userId);

        Task<string> SignUp(SignUpDTO input);
        Task<string> Verification(VerificationDTO input);

        Task<bool> ResetPassword(ResetPasswordDTO input);
    }
}
