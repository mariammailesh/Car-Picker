using Car_Picker_API.DTOs.Car_Review_DTO.Response;
using Car_Picker_API.DTOs.Review_DTO.Request;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Interfaces
{
    public interface ICarReview
    {
        Task<CarReview> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<GetCarReviewDTO>> GetAllReviewsByCarIdAsync(int carId);
        Task<string> CreateReviewAsync(CreateCarReviewDTO input);
        Task<string> UpdateReviewAsync(CreateCarReviewDTO updatedReview);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<float> GetAverageRatingForCarAsync(int carId);
        Task<bool> UpdateReviewStatusAsync(int reviewId, ReviewStatus newStatus);

    }
}
