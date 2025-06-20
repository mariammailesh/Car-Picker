using Car_Picker_API.DTOs;

namespace Car_Picker_API.Interfaces
{
    public interface IUserAuthenticationInterface
    {
        Task<string> SignIn(SignInputDTO input);
        Task<bool> SendOTP(string email);

        Task<bool> SignOut(int userId);
    }
}
