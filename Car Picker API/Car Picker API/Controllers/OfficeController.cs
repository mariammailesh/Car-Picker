﻿using Car_Picker_API.Helpers.Enums;
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
                {
                    return NotFound(new
                    {
                        message = "No offices found."
                    });
                }

                return Ok(new
                {
                    message = "Offices retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving offices",
                    details = ex.Message
                });
            }
        }


        [HttpGet("by-category/{category}")]
        public async Task<IActionResult> GetOfficesByCategory(OfficesCategory category)
        {
            try
            {
                var offices = await _officeService.GetOfficesByCategoryAsync(category);

                if (offices == null || !offices.Any())
                {
                    return NotFound(new
                    {
                        message = "No offices found for this category."
                    });
                }

                return Ok(new
                {
                    message = "Offices retrieved successfully",
                    data = offices
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving offices",
                    details = ex.Message
                });
            }
        }



        [HttpGet("Get-Offices-Info")]
        public async Task<IActionResult> GetOfficesIfo()
        {
            try
            {
                var result = await _officeService.GetOfficesInfo(); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
 


    }
}
