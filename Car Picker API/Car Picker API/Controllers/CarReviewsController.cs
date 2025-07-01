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
        public CarReviewsController(ICarReview carReviewService)
        {
            _carReviewService = carReviewService;
        }

        [HttpGet("Get-Review-By-Id/{reviewId}")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            try
            {
                var review = await _carReviewService.GetReviewByIdAsync(reviewId);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving review",
                    details = ex.Message
                });
            }
        }


        [HttpGet("Get-All-Reviews-By-CarId/{carId}")]
        public async Task<IActionResult> GetAllReviewsByCarId(int carId)
        {
            try
            {
                List<ResponseCarReviewDTO> reviews = await _carReviewService.GetAllReviewsByCarIdAsync(carId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving reviews",
                    details = ex.Message
                });
            }
        }


        [HttpPost("Create-Review")]
        public async Task<IActionResult> CreateReview([FromBody] RequestCarReviewDTO input)
        {
            try
            {
                string response = await _carReviewService.CreateReviewAsync(input);

                return StatusCode(201, new
                {
                    message = "Review created successfully",
                    data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }


        [HttpPut("Update-Review")]
        public async Task<IActionResult> UpdateReview([FromBody] RequestCarReviewDTO updatedReview)
        {
            try
            {
                string response = await _carReviewService.UpdateReviewAsync(updatedReview);

                return Ok(new
                {
                    message = "Review updated successfully",
                    data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error updating review",
                    details = ex.Message
                });
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
                    return Ok(new
                    {
                        message = "Review deleted successfully"
                    });
                }

                return NotFound(new
                {
                    message = "Review not found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error deleting review",
                    details = ex.Message
                });
            }
        }


        [HttpGet("Get-Average-Rating-For-Car/{carId}")]
        public async Task<IActionResult> GetAverageRatingForCar(int carId)
        {
            try
            {
                float averageRating = await _carReviewService.GetAverageRatingForCarAsync(carId);

                return Ok(new
                {
                    message = "Average rating retrieved successfully",
                    data = averageRating
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving average rating",
                    details = ex.Message
                });
            }
        }


        [HttpPut("Update-Review-Status/{reviewId}")]
        //[Authorize(Roles = "1,2")]
        public async Task<IActionResult> UpdateReviewStatus(int reviewId, [FromBody] ReviewStatus newStatus)
        {
            try
            {
                bool isUpdated = await _carReviewService.UpdateReviewStatusAsync(reviewId, newStatus);

                if (isUpdated)
                {
                    return Ok(new
                    {
                        message = "Review status updated successfully"
                    });
                }

                return NotFound(new
                {
                    message = "Review not found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error updating review status",
                    details = ex.Message
                });
            }
        }

    }
}
