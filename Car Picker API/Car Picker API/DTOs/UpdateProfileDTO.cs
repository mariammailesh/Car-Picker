using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs
{
    public class UpdateProfileDTO
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}
