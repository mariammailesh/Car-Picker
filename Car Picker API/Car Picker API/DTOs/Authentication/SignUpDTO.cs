using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs.Authentication
{
    public class SignUpDTO
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public IFormFile DrivingLicenseFrontImage { get; set; }
        public IFormFile DrivingLicenseBackImage { get; set; }
        public IFormFile NationalIDFrontImage { get; set; }
        public IFormFile NationalIDBackImage { get; set; }
    }
}
