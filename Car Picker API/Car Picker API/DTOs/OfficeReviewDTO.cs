namespace Car_Picker_API.DTOs
{
    public class OfficeReviewDTO
    {
        public int Id { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewContent { get; set; }
        public string ReviewStatus { get; set; }
        public int StarsReview { get; set; }

        public int OfficeId { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; } 
    }
}
