using Car_Picker_API.DTOs.Office_Review_DTO.Request;
using Car_Picker_API.DTOs.Office_Review_DTO.Response;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeReviewsController : ControllerBase
    {
        private readonly IOfficeReview _officeReviewService;
        public OfficeReviewsController(IOfficeReview officeReviewService)
        {
            _officeReviewService = officeReviewService;
        }


        [HttpGet("Get-Review-By-Id/{reviewId}")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            try
            {
                var review = await _officeReviewService.GetReviewByIdAsync(reviewId);
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


        [HttpGet("Get-All-Reviews-By-OfficeId/{officeId}")]
        public async Task<IActionResult> GetAllReviewsByOfficeId(int officeId)
        {
            try
            {
                IEnumerable<ResponseOfficeReviewDTO> reviews = await _officeReviewService.GetAllReviewsByOfficeIdAsync(officeId);
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
        public async Task<IActionResult> CreateReview([FromBody] RequestOfficeReviewDTO input)
        {
            try
            {
                string response = await _officeReviewService.CreateReviewAsync(input);

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
        public async Task<IActionResult> UpdateReview([FromBody] RequestOfficeReviewDTO updatedReview)
        {
            try
            {
                string response = await _officeReviewService.UpdateReviewAsync(updatedReview);

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
                bool isDeleted = await _officeReviewService.DeleteReviewAsync(reviewId);

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


        [HttpGet("Get-Average-Rating-For-Office/{officeId}")]
        public async Task<IActionResult> GetAverageRatingForOffice(int officeId)
        {
            try
            {
                float averageRating = await _officeReviewService.GetAverageRatingForOfficeAsync(officeId);

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
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> UpdateReviewStatus(int reviewId, [FromBody] ReviewStatus newStatus)
        {
            try
            {
                bool isUpdated = await _officeReviewService.UpdateReviewStatusAsync(reviewId, newStatus);

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

        [HttpGet("Get-Review-Count-By-OfficeId/{officeId}")]
        public async Task<IActionResult> GetReviewCountByOfficeId(int officeId)
        {
            try
            {
                var reviews = await _officeReviewService.GetAllReviewsByOfficeIdAsync(officeId);
                int count = reviews?.Count() ?? 0;

                return Ok(new
                {
                    message = "Review count retrieved successfully",
                    data = count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving review count",
                    details = ex.Message
                });
            }
        }
    }
}
