using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class User:ParentEntity
    {

        public string FullName { get; set; }
        public string Username { get; set; } //hashed
        public string Email { get; set; } //hashed
        public string Password { get; set; }//hashed
        public string PhoneNumber { get; set; }
        public Role RoleId { get; set; } = Role.Client; // Enum for Role stores 3
        public bool IsLogedIn { get; set; } = false;
        public string? OTPCode { get; set; }
        public DateTime? OTPExpiry { get; set; }
        public bool IsVerified { get; set; } = false;
        public Gender Gender { get; set; } = Gender.WouldPreferNotToSay;
        public DateOnly DateOfBirth { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string DrivingLicenseFrontImagePath { get; set; }
        public string DrivingLicenseBackImagePath { get; set; } 
        public string NationalIDFrontImagePath { get; set; }
        public string NationalIDBackImagePath { get; set; }

    }
}
