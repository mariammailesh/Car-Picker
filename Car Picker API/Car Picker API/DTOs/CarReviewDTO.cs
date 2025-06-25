using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs
{
    public class CarReviewDTO
    {
        
        public string ReviewContent { get; set; }
        public ReviewStatus ReviStatus { get; set; } = ReviewStatus.Pending;
        public int RatingAmount { get; set; } = 1;
        public string UserName { get; set; }
        public DateTime Date { get; set; }

 

    }
}
