namespace Car_Picker_API.DTOs.Authentication
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string OTPCode { get; set; }
    }
}
