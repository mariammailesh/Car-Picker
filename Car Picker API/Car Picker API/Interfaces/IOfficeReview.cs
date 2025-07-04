﻿using Car_Picker_API.DTOs.Car_Review_DTO.Response;
using Car_Picker_API.DTOs.Office_Review_DTO.Request;
using Car_Picker_API.DTOs.Office_Review_DTO.Response;
using Car_Picker_API.DTOs.Review_DTO.Request;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Interfaces
{
    public interface IOfficeReview
    {
        Task<ResponseOfficeReviewDTO> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<ResponseOfficeReviewDTO>> GetAllReviewsByOfficeIdAsync(int officeId);//also applied in the count all reviews
        Task<string> CreateReviewAsync(RequestOfficeReviewDTO input);
        Task<string> UpdateReviewAsync(RequestOfficeReviewDTO updatedReview);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<float> GetAverageRatingForOfficeAsync(int officeId);
        Task<bool> UpdateReviewStatusAsync(int reviewId, ReviewStatus newStatus);
    }
}
