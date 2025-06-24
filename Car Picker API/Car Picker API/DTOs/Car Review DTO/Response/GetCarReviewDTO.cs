namespace Car_Picker_API.DTOs.Car_Review_DTO.Response
{
    public class GetCarReviewDTO
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public short RatingAmount { get; set; } 
        public string? Comment { get; set; }
    }
}
