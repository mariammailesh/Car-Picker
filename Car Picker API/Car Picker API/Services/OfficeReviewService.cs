using Car_Picker_API.DTOs.Office_Review_DTO.Request;
using Car_Picker_API.DTOs.Office_Review_DTO.Response;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Helpers.Review_Validation;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class OfficeReviewService : IOfficeReview
    {
        private readonly CarPickerDbContext _context;

        public OfficeReviewService(CarPickerDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateReviewAsync(RequestOfficeReviewDTO input)
        {
            OfficeReview officeReview = new OfficeReview();
            if (ReviewValidation.ValidateRatingAmount(input.RatingAmount))
            {
                officeReview.ReviewContent = input.Comment;
                officeReview.RatingAmount = input.RatingAmount;
                officeReview.OfficeId = input.OfficeId;
                officeReview.UserId = input.UserId;
                _context.OfficeReviews.Add(officeReview);
                await _context.SaveChangesAsync();
                return "Office Review Created Successfully";
            }
            else
            {
                throw new ArgumentException("Invalid review data.");
            }

        } //done

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Invalid review ID.");

            var review = await _context.OfficeReviews.FindAsync(reviewId);
            if (review == null)
                return false;

            _context.OfficeReviews.Remove(review);
            await _context.SaveChangesAsync();

            return true;
        } //done

        public async Task<IEnumerable<ResponseOfficeReviewDTO>> GetAllReviewsByOfficeIdAsync(int officeId)
        {
            var reviews = await _context.OfficeReviews.Where(r => r.OfficeId == officeId).SingleOrDefaultAsync();
            if (reviews == null)
                throw new KeyNotFoundException("Review was not foung.");

            var getReviewsList = (from Reviews in _context.OfficeReviews
                                  where Reviews.OfficeId == officeId && Reviews.ReviewStatus == ReviewStatus.Approved
                                  select new ResponseOfficeReviewDTO
                                  {
                                      UserId = Reviews.UserId,
                                      FullName = Reviews.User.FullName,
                                      OfficeId = Reviews.OfficeId,
                                      RatingAmount = Reviews.RatingAmount,
                                      Comment = Reviews.ReviewContent
                                  }).ToList();
            return getReviewsList;
        } //done

        public async Task<float> GetAverageRatingForOfficeAsync(int officeId)
        {
            if (officeId <= 0)
                throw new ArgumentException("Invalid office ID.");

            bool officeExists = await _context.Offices.AnyAsync(c => c.Id == officeId);
            if (!officeExists)
                throw new KeyNotFoundException($"Office with ID {officeId} does not exist.");

            var reviews = await _context.OfficeReviews
                .Where(r => r.OfficeId == officeId && r.ReviewStatus == ReviewStatus.Approved)
                .ToListAsync();

            if (reviews.Count == 0)
                return 0.0f;

            var average = reviews.Average(r => (int)r.RatingAmount);
            return (float)Math.Round(average, 2);
        } //done

        public async Task<ResponseOfficeReviewDTO> GetReviewByIdAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Invalid review ID.");

            var review = await _context.OfficeReviews
                .Where(r => r.Id == reviewId && r.ReviewStatus == ReviewStatus.Approved)
                .Select(r => new ResponseOfficeReviewDTO
                {
                    UserId = r.UserId,
                    FullName = r.User.FullName,
                    OfficeId = r.OfficeId,
                    RatingAmount = r.RatingAmount,
                    Comment = r.ReviewContent,
                }).SingleOrDefaultAsync();

            if (review == null)
                throw new KeyNotFoundException($"Review with ID {reviewId} not found.");

            return review;
        } //done


        public async Task<string> UpdateReviewAsync(RequestOfficeReviewDTO updatedReview)
        {
            if (updatedReview == null)
                throw new ArgumentException("Invalid review data.");

            // Find the review for the given office and user
            var existingReview = await _context.OfficeReviews
                .FirstOrDefaultAsync(r => r.UserId == updatedReview.UserId && r.OfficeId == updatedReview.OfficeId);
            if (existingReview == null)
                throw new KeyNotFoundException($"Review not found.");

            existingReview.ReviewContent = updatedReview.Comment;
            existingReview.RatingAmount = updatedReview.RatingAmount;

            await _context.SaveChangesAsync();

            return "Review Updated Successfully!";
        } //done

        public async Task<bool> UpdateReviewStatusAsync(int reviewId, ReviewStatus newStatus)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Invalid review ID.");
            if (!ReviewValidation.ValidateReviewStatus(newStatus))
                throw new ArgumentException("Invalid review status.");
            var review = await _context.OfficeReviews.FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {reviewId} not found.");

            review.ReviewStatus = newStatus;
            review.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        } //done
    }
}
