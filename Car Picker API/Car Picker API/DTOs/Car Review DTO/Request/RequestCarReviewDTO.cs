using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs.Review_DTO.Request
{
    public class RequestCarReviewDTO
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public short RatingAmount { get; set; } //refers to an Enum form 1-5 stars
        public string? Comment { get; set; } // Optional comment about the review

        
    }
}
