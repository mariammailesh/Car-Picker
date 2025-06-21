using Car_Picker_API.DTOs;
using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationInterface _appService;

        public AuthenticationController(IUserAuthenticationInterface appService)
        {
            _appService = appService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInputDTO input)
        {
            try
            {
                var response = await _appService.SignIn(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SendOTP(string email)
        {
            try
            {
                var response = await _appService.SendOTP(email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> SignOut(int userId)
        {
            try
            {
                var response = await _appService.SignOut(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO input)
        {
            try
            {
                var response = await _appService.SignUp(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Verification(VerificationDTO input)
        {
            try
            {
                var response = await _appService.Verification(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO input)
        {
            try

            {
                var response = await _appService.ResetPassword(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
