using Car_Picker_API.DTOs.Car_Review_DTO.Response;
using Car_Picker_API.DTOs.Review_DTO.Request;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Interfaces
{
    public interface ICarReview
    {
        Task<ResponseCarReviewDTO> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<ResponseCarReviewDTO>> GetAllReviewsByCarIdAsync(int carId);
        Task<string> CreateReviewAsync(RequestCarReviewDTO input);
        Task<string> UpdateReviewAsync(RequestCarReviewDTO updatedReview);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<float> GetAverageRatingForCarAsync(int carId);
        Task<bool> UpdateReviewStatusAsync(int reviewId, ReviewStatus newStatus);

    }
}
