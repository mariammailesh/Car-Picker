namespace Car_Picker_API.DTOs
{
    public class ResetPasswordDTO
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string OTPCode { get; set; }
    }
}
