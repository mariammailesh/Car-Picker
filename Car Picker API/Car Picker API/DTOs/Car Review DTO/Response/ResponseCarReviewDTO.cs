namespace Car_Picker_API.DTOs.Car_Review_DTO.Response
{
    public class ResponseCarReviewDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } // Full name of the user leaving the 
        public int CarId { get; set; }
        public short RatingAmount { get; set; } 
        public string? Comment { get; set; }
    }
}
