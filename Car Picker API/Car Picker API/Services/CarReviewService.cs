using Car_Picker_API.DTOs.Car_Review_DTO.Response;
using Car_Picker_API.DTOs.Review_DTO.Request;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Helpers.Review_Validation;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class CarReviewService : ICarReview
    {
        private readonly CarPickerDbContext _context;

        public CarReviewService(CarPickerDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseCarReviewDTO> GetReviewByIdAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Invalid review ID.");

            var review = await _context.CarReviews
                .Where(r => r.Id == reviewId && r.ReviewStatus == ReviewStatus.Approved)
                .Select(r => new ResponseCarReviewDTO
                {
                    UserId = r.UserId,
                    FullName = r.User.FullName,
                    CarId = r.CarId,
                    RatingAmount = r.RatingAmount,
                    Comment = r.ReviewContent,
                }).SingleOrDefaultAsync();

            if (review == null)
                throw new KeyNotFoundException($"Review with ID {reviewId} not found.");

            return review;
        } //done

        public async Task<List<ResponseCarReviewDTO>> GetAllReviewsByCarIdAsync(int carId)
        {
            var reviews = await _context.CarReviews.Where(r => r.CarId == carId).SingleOrDefaultAsync();
            if (reviews == null)
                throw new KeyNotFoundException("Review was not foung.");
            
            var getReviewsList = (from Reviews in _context.CarReviews
                                  where Reviews.CarId == carId && Reviews.ReviewStatus == ReviewStatus.Approved
                                  select new ResponseCarReviewDTO
                                  {
                                      UserId = Reviews.UserId,
                                      FullName = Reviews.User.FullName,
                                      CarId = Reviews.CarId,
                                      RatingAmount = Reviews.RatingAmount,
                                      Comment = Reviews.ReviewContent
                                  }).ToList();
            return getReviewsList;
        }//done

        public async Task<string> CreateReviewAsync(RequestCarReviewDTO input)
        {
            CarReview carReview = new CarReview();
           if(ReviewValidation.ValidateRatingAmount(input.RatingAmount))
            {
                carReview.ReviewContent = input.Comment;
                carReview.RatingAmount = input.RatingAmount;
                carReview.CarId = input.CarId;
                carReview.UserId = input.UserId;
                carReview.CreatedBy = input.UserId.ToString(); // Assuming the user creating the review is the same as the one providing the input

                _context.CarReviews.Add(carReview);
                await _context.SaveChangesAsync();
                return "Car Review Created Successfully";
            }
            else
            {
                throw new ArgumentException("Invalid review data.");
            }

        }//done

        public async Task<string> UpdateReviewAsync(RequestCarReviewDTO updatedReview)
        {
            if (updatedReview == null)
                throw new ArgumentException("Invalid review data.");

            // Find the review for the given car and user
            var existingReview = await _context.CarReviews
                .FirstOrDefaultAsync(r => r.UserId == updatedReview.UserId && r.CarId == updatedReview.CarId);
            if (existingReview == null)
                throw new KeyNotFoundException($"Review not found.");

            existingReview.ReviewContent = updatedReview.Comment;
            existingReview.RatingAmount = updatedReview.RatingAmount;

            await _context.SaveChangesAsync();

            return "Review Updated Successfully!";
        } //done

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Invalid review ID.");

            var review = await _context.CarReviews.FindAsync(reviewId);
            if (review == null)
                return false;

            _context.CarReviews.Remove(review);
            await _context.SaveChangesAsync();

            return true;
        } //done

        public async Task<float> GetAverageRatingForCarAsync(int carId)
        {
            if (carId <= 0)
                throw new ArgumentException("Invalid car ID.");

            bool carExists = await _context.Cars.AnyAsync(c => c.Id == carId);
            if (!carExists)
                throw new KeyNotFoundException($"Car with ID {carId} does not exist.");

            var reviews = await _context.CarReviews
                .Where(r => r.CarId == carId && r.ReviewStatus == ReviewStatus.Approved)
                .ToListAsync();

            if (reviews.Count == 0)
                return 0.0f;

            var average = reviews.Average(r => (int)r.RatingAmount);
            return (float)Math.Round(average, 2);
        } //done
        public async Task<bool> UpdateReviewStatusAsync(int reviewId, ReviewStatus newStatus)
        {
            if (reviewId <= 0)
                throw new ArgumentException("Invalid review ID.");
            if(!ReviewValidation.ValidateReviewStatus(newStatus))
                throw new ArgumentException("Invalid review status.");
            var review = await _context.CarReviews.FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {reviewId} not found.");

            review.ReviewStatus = newStatus;
            review.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }//done
    }
}
