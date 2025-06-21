using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class OfficeReview : ParentEntity
    {
        public string ReviewTitle { get; set; }
        public string ReviewContent { get; set; } // Content of the review
        public ReviewStatus ReviStatus { get; set; } = ReviewStatus.Pending; // Enum for review status (Pending, Approved, Rejected)
        public StarsReview StarsReview { get; set; } = StarsReview.Average;// Enum for star rating (1-5 stars)

        // Foreign Keys
        public int OfficeId { get; set; } // Foreign Key to Office entity
        public int UserId { get; set; } // Foreign Key to User entity

        // Navigation properties
        public Office Office { get; set; } // Navigation property to the Office entity
        public User User { get; set; } // Navigation property to the User entity

    }
}
