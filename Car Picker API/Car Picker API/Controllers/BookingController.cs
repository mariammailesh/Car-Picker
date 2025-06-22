using Car_Picker_API.DTOs;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using Car_Picker_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingInterface _bookingService;
        public BookingController(IBookingInterface bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO input)
        {
            try
            {
                var response = await _bookingService.CreateBooking(input);
                return Ok(new
                {
                    message = "Booking created successfully",
                    data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error creating booking",
                    details = ex.Message
                });
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMyBookings(int userId)
        {
            try
            {
                var result = await _bookingService.GetMyBookings(userId);
                return Ok(new
                {
                    message = "Booking retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving booking",
                    details = ex.Message
                });
            }
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateBookingStatus(int reservationId, ReservationStatus newStatus)
        {
            try
            {
                var result = await _bookingService.UpdateBookingStatus(reservationId, newStatus);
                return Ok(new
                {
                    message = "Booking status updated successfully",
                    data = result
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating booking status", details = ex.Message });
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBookingRequests()
        {
            try
            {
                var result = await _bookingService.GetAllBookingRequests();
                return Ok(new
                {
                    message = "Booking requests retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving booking requests",
                    details = ex.Message
                });
            }
        }


        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteBooking(int reservationId)
        {
            try
            {
                var response = await _bookingService.DeleteBooking(reservationId);
                return Ok(new
                {
                    message = "Booking deleted successfully",
                    data = response
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error deleting booking",
                    details = ex.Message
                });
            }

        }

    }
}
