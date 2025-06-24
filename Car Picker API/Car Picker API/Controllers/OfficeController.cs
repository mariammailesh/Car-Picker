using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }


        //Get All Offices 
        [HttpGet]
        public async Task<IActionResult> GetAllOffices()
        {
            try
            {
                var result = await _officeService.GetAllOfficesAsync();

                if (result == null || !result.Any())
                    return NotFound("No offices found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("by-category/{category}")]
        public async Task<IActionResult> GetOfficesByCategory(OfficesCategory category)
        {
            try
            {
                var offices = await _officeService.GetOfficesByCategoryAsync(category);
                if (offices == null || !offices.Any())
                    return NotFound("No offices found for this category.");

                return Ok(offices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("office/{officeId}")]
        public async Task<IActionResult> GetReviewsByOfficeId(int officeId)
        {
            try
            {
                var reviews = await _officeService.GetOfficeReviewsByOfficeIdAsync(officeId);

                if (reviews == null || !reviews.Any())
                {
                    return NotFound($"No reviews found for office with ID {officeId}.");
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
