using Car_Picker_API.DTOs;
using CarPicker_API.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly CarPickerDbContext _context;

        public ProfileController(CarPickerDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProfile/{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new {
                    u.FullName,
                    u.Email,
                    u.PhoneNumber,
                    u.Gender,
                    u.DateOfBirth,
                    u.NationalIDFrontImagePath,
                    u.NationalIDBackImagePath,
                    u.DrivingLicenseFrontImagePath,
                    u.DrivingLicenseBackImagePath,
                    u.IsVerified
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpPut("UpdateProfile/{userId}")]
        public async Task<IActionResult> UpdateProfile(int userId, [FromBody] UpdateProfileDTO input)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found");

            user.FullName = input.FullName ?? user.FullName;
            user.PhoneNumber = input.PhoneNumber ?? user.PhoneNumber;

            if (input.Gender.HasValue)
                user.Gender = input.Gender.Value;

            user.Email = input.Email ?? user.Email;

            if (input.DateOfBirth.HasValue)
                user.DateOfBirth = input.DateOfBirth.Value;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully" });
        }

    }
}
