using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class CarReview :ParentEntity
    {
        public string ReviewTitle { get; set; }
        public string ReviewContent { get; set; } // Content of the review
        public ReviewStatus ReviStatus { get; set; } = ReviewStatus.Pending; // Enum for review status (Pending, Approved, Rejected)
        public StarsReview StarsReview { get; set; } = StarsReview.Average;// Enum for star rating (1-5 stars)
        public int CarId { get; set; } // Foreign Key to Car entity
        public int UserId { get; set; } // Foreign Key to User entity
    }
}
