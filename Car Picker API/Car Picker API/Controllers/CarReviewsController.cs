using Car_Picker_API.DTOs.Car_Review_DTO.Response;
using Car_Picker_API.DTOs.Review_DTO.Request;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarReviewsController : ControllerBase
    {
        private readonly ICarReview _carReviewService;
        private readonly IOfficeReview _officeReviewService;
        public CarReviewsController(ICarReview carReviewService)
        {
            _carReviewService = carReviewService;
        }
        public CarReviewsController(IOfficeReview officeReviewService)
        {
            _officeReviewService = officeReviewService;
        }


        [HttpGet("Get-Review-By-Id/{reviewId}")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            try
            {
                var review = await _carReviewService.GetReviewByIdAsync(reviewId);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Get-All-Reviews-By-CarId/{carId}")]
        public async Task<IActionResult> GetAllReviewsByCarId(int carId)
        {
            try
            {
                IEnumerable<ResponseCarReviewDTO> reviews = await _carReviewService.GetAllReviewsByCarIdAsync(carId);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Create-Review")]
        public async Task<IActionResult> CreateReview([FromBody] RequestCarReviewDTO input)
        {
            try
            {
                string response = await _carReviewService.CreateReviewAsync(input);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("Update-Review")]
        public async Task<IActionResult> UpdateReview([FromBody] RequestCarReviewDTO updatedReview)
        {
            try
            {
                string response = await _carReviewService.UpdateReviewAsync(updatedReview);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("Delete-Review/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            try
            {
                bool isDeleted = await _carReviewService.DeleteReviewAsync(reviewId);
                if (isDeleted)
                {
                    return StatusCode(200, "Review deleted successfully.");
                }
                return StatusCode(404, "Review not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Get-Average-Rating-For-Car/{carId}")]
        public async Task<IActionResult> GetAverageRatingForCar(int carId)
        {
            try
            {
                float averageRating = await _carReviewService.GetAverageRatingForCarAsync(carId);
                return StatusCode(200, averageRating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("Update-Review-Status/{reviewId}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> UpdateReviewStatus(int reviewId, [FromBody] ReviewStatus newStatus)
        {
            try
            {
                bool isUpdated = await _carReviewService.UpdateReviewStatusAsync(reviewId, newStatus);
                if (isUpdated)
                {
                    return StatusCode(200, "Review status updated successfully.");
                }
                return StatusCode(404, "Review not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
