using Car_Picker_API.DTOs;
using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentInterface _appService;
        public PaymentController(IPaymentInterface appService)
        {
            _appService = appService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddPaymentMethod([FromBody] AddPaymentDTO input)
        {
            try
            {
                var response = await _appService.AddPaymentMethod(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var result = await _appService.GetAllPayments();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
