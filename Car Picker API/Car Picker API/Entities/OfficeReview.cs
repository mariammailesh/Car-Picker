using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class OfficeReview : ParentEntity
    {
       
        public string ReviewContent { get; set; } // Content of the review
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.Pending; // Enum for review status (Pending, Approved, Rejected)
        public short RatingAmount { get; set; } = 1;

        public int TotalReviewsForOffice { get; set; }
        // Foreign Keys
        public int OfficeId { get; set; } // Foreign Key to Office entity
        public int UserId { get; set; } // Foreign Key to User entity

        // Navigation properties
        public Office Office { get; set; } // Navigation property to the Office entity
        public User User { get; set; } // Navigation property to the User entity

    }
}
