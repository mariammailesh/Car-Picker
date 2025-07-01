using Car_Picker_API.DTOs;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarServices _carService;

        public CarController(ICarServices carService)
        {
            _carService = carService;
        }


        //Get Car By Category 
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetCarsByCategory(int categoryId)
        {
            try
            {
                var cars = await _carService.GetCarsByCategoryAsync(categoryId);

                if (cars == null || !cars.Any())
                {
                    return NotFound(new
                    {
                        message = "No cars found for this category."
                    });
                }

                return Ok(new
                {
                    message = "Cars retrieved successfully",
                    data = cars
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving cars",
                    details = ex.Message
                });
            }
        }




        //Get Car By Office 
        [HttpGet("by-office/{officeId}")]
        public async Task<IActionResult> GetCarsByOffice(int officeId)
        {
            try
            {
                var cars = await _carService.GetCarsByOfficeId(officeId);
                if (cars == null || !cars.Any())
                {
                    return NotFound(new
                    {
                        message = "No cars found for this office."
                    });
                }

                return Ok(new
                {
                    message = "Cars retrieved successfully",
                    data = cars
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving cars",
                    details = ex.Message
                });
            }
        }

        [HttpGet("{carId}/info")]
        public async Task<IActionResult> GetCarGeneralInfo(int carId)
        {
            try
            {
                var carInfo = await _carService.GetCarGeneralInfo(carId);

                if (carInfo == null)
                {
                    return NotFound(new
                    {
                        message = $"Car with Id = {carId} not found."
                    });
                }

                return Ok(new
                {
                    message = "Car info retrieved successfully",
                    data = carInfo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving car info",
                    details = ex.Message
                });
            }
        }



        [HttpGet("{carId}/images")]
        public async Task<IActionResult> GetCarImages(int carId)
        {
            try
            {
                var result = await _carService.GetCarImages(carId);

                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        message = "No images found for this car."
                    });
                }

                return Ok(new
                {
                    message = "Car images retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving car images",
                    details = ex.Message
                });
            }
        }



        [HttpGet("{carId}/specs")]
        public async Task<IActionResult> GetCarSpecs(int carId)
        {
            try
            {
                var result = await _carService.GetCarSpecs(carId);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Car specifications not found."
                    });
                }

                return Ok(new
                {
                    message = "Car specifications retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving car specifications",
                    details = ex.Message
                });
            }
        }



        [HttpGet("{carId}/average-price")]
        public async Task<IActionResult> GetCarAveragePrice(int carId)
        {
            try
            {
                var result = await _carService.GetCarAveragePrice(carId);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Average price not found for this car."
                    });
                }

                return Ok(new
                {
                    message = "Average price retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving average price",
                    details = ex.Message
                });
            }
        }




        [HttpGet("{carId}/performance-score")]
        public async Task<IActionResult> GetCarPerformanceScore(int carId)
        {
            try
            {
                var result = await _carService.GetCarPerformanceScore(carId);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message = "Performance score not found for this car."
                    });
                }

                return Ok(new
                {
                    message = "Performance score retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving performance score",
                    details = ex.Message
                });
            }
        }



        [HttpGet("for-sale")]
        public async Task<IActionResult> GetCarsForSale()
        {
            try
            {
                var carsForSale = await _carService.GetCarsForSale();

                if (carsForSale == null || !carsForSale.Any())
                {
                    return NotFound(new
                    {
                        message = "No cars available for sale."
                    });
                }

                return Ok(new
                {
                    message = "Cars for sale retrieved successfully",
                    data = carsForSale
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving cars for sale",
                    details = ex.Message
                });
            }
        }




        [HttpPost("check-availability")]
        public async Task<IActionResult> CheckCarAvailability([FromBody] CheckCarAvailabilityRequest request)
        {
            try
            {
                bool isAvailable = await _carService.CheckCarAvailability(request.CarId, request.StartDate, request.EndDate);

                return Ok(new
                {
                    message = "Availability checked successfully",
                    data = new
                    {
                        CarId = request.CarId,
                        IsAvailable = isAvailable
                    }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = "Invalid input",
                    details = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error checking car availability",
                    details = ex.Message
                });
            }
        }



        [HttpGet("filter")]
        public async Task<IActionResult> FilterCars(
            [FromQuery] OfficesCategory category,
            [FromQuery] SortByOption sortBy,
            [FromQuery] bool descending = false)
        {
            try
            {
                var result = await _carService.GetCarsByCategoryAsync(category, sortBy, descending);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error filtering cars",
                    details = ex.Message
                });
            }
        }



        [HttpGet("ForRent")]
        public async Task<IActionResult> GetCarsForRent()
        {
            try
            {
                var cars = await _carService.GetCarsForRent();

                if (cars == null || !cars.Any())
                {
                    return NotFound(new
                    {
                        message = "No cars available for rent."
                    });
                }

                return Ok(new
                {
                    message = "Cars for rent retrieved successfully",
                    data = cars
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error retrieving cars for rent",
                    details = ex.Message
                });
            }
        }
    }

}

