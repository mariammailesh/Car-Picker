using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoriesInterface _categoriesServices;

        public CategoriesController(ICategoriesInterface categoriesServices)
        {
            _categoriesServices = categoriesServices;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var response = await _categoriesServices.GetAllCategories();
                return Ok(new
                {
                    message = " categories retrieved successfully",
                    data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieved categories",
                    details = ex.Message
                });
            }
        }
    }
}
