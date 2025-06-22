namespace Car_Picker_API.DTOs.Authentication
{
    public class VerificationDTO
    {
        public string Email { get; set; }

        public string OTPCode { get; set; }

        public bool IsSignup { get; set; }
    }
}
