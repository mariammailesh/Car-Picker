using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs
{
    public class SignUpDTO
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string DrivingLicenseFrontImagePath { get; set; }
        public string DrivingLicenseBackImagePath { get; set; }
        public string NationalIDFrontImagePath { get; set; }
        public string NationalIDBackImagePath { get; set; }
    }
}
