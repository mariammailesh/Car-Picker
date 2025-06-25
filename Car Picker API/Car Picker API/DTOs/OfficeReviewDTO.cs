using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs
{
    public class OfficeReviewDTO
    {
        public int Id { get; set; }
        
        public string ReviewStatus { get; set; }

        public short RatingAmount { get; set; } = 1;

        public int OfficeId { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }
        public int TotalReviewsForOffice { get; set; }

    }
}
