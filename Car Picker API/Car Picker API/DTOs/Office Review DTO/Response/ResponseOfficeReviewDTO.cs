namespace Car_Picker_API.DTOs.Office_Review_DTO.Response
{
    public class ResponseOfficeReviewDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = "User"; // Full name of the user leaving the 
        public int OfficeId { get; set; }
        public short RatingAmount { get; set; }
        public string? Comment { get; set; }
    }
}
