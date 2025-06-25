namespace Car_Picker_API.DTOs.Office_Review_DTO.Request
{
    public class RequestOfficeReviewDTO
    {
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public short RatingAmount { get; set; } //refers to an Enum form 1-5 stars
        public string? Comment { get; set; } // Optional comment about the review
    }
}
