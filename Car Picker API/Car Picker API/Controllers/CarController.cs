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
                    return NotFound("No cars found for this category.");

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        //Get Car By Office 
        [HttpGet("by-office/{officeId}")]
        public async Task<IActionResult> GetCarsByOffice(int officeId)
        {
            try
            {
                var cars = await _carService.GetCarsByOfficeIdForSale(officeId);
                if (cars == null || !cars.Any())
                    return NotFound("No cars found for this office.");

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





        [HttpGet("{carId}/info")]
        public async Task<IActionResult> GetCarGeneralInfo(int carId)
        {
            try
            {
                var carInfo = await _carService.GetCarGeneralInfo(carId);
                if (carInfo == null)
                    return NotFound($"Car with Id = {carId} not found.");

                return Ok(carInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }





        [HttpGet("{carId}/images")]
        public async Task<IActionResult> GetCarImages(int carId)
        {
            try
            {
                var result = await _carService.GetCarImages(carId);
                if (result == null || !result.Any())
                    return NotFound("No images found for this car.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("{carId}/specs")]
        public async Task<IActionResult> GetCarSpecs(int carId)
        {
            try
            {
                var result = await _carService.GetCarSpecs(carId);
                if (result == null)
                    return NotFound("Car specifications not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("{carId}/average-price")]
        public async Task<IActionResult> GetCarAveragePrice(int carId)
        {
            try
            {
                var result = await _carService.GetCarAveragePrice(carId);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("{carId}/performance-score")]
        public async Task<IActionResult> GetCarPerformanceScore(int carId)
        {
            try
            {
                var result = await _carService.GetCarPerformanceScore(carId);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("for-sale")]
        public async Task<IActionResult> GetCarsForSale()
        {
            try
            {
                var carsForSale = await _carService.GetCarsForSale();
                return Ok(carsForSale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpPost("check-availability")]
        public async Task<IActionResult> CheckCarAvailability([FromBody] CheckCarAvailabilityRequest request)
        {
            try
            {
                bool isAvailable = await _carService.CheckCarAvailability(request.CarId, request.StartDate, request.EndDate);
                return Ok(new { CarId = request.CarId, IsAvailable = isAvailable });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(500, ex.Message);
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
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("ForRent")]
        public async Task<IActionResult> GetCarsForRent()
        {
            try
            {
                var cars = await _carService.GetCarsForRent();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       


        [HttpGet("Get-Rent-Cars-By-Office/{officeId}")]
        public async Task<IActionResult> GetCarsForRentByOfficeId(int officeId)
        {
            try
            {
                var cars = await _carService.GetCarsForRentByOfficeId(officeId);

                if (cars == null || !cars.Any())
                    return NotFound($"No rental cars found for Office with ID {officeId}.");

                return Ok(cars);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }





    }

}

