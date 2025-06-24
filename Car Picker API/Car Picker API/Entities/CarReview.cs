using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class CarReview :ParentEntity
    {
       
        public string? ReviewContent { get; set; } // Content of the review
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.Pending; // Enum for review status (Pending, Approved, Rejected)
        public short RatingAmount { get; set; } = 1; // Rating amount, default is 1 star, can be between 1 and 5 stars

        //Foreign Keys
        public int CarId { get; set; } // Foreign Key to Car entity
        public int UserId { get; set; } // Foreign Key to User entity
        
        // Navigation properties
        public User User { get; set; } // Navigation property to User entity
        public Car Car { get; set; } // Navigation property to Car entity

    }
}
