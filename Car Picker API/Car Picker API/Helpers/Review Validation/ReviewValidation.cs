using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Helpers.Review_Validation
{
    public static class ReviewValidation
    {

        public static bool ValidateRatingAmount(short rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");
            return true;
        }
        
        public static bool ValidateReviewStatus(ReviewStatus status)
        {
            if (!Enum.IsDefined(typeof(ReviewStatus), status))
                throw new ArgumentException("Invalid review status.");
            return true;
        }
    }
}
