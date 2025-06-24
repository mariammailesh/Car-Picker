using Car_Picker_API.DTOs;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class CarServices : ICarServices
    {
        private readonly CarPickerDbContext _context;

        public CarServices(CarPickerDbContext context)
        {

            _context = context;
        }



        //Get Cars By Category 
        public async Task<List<CarByCategoryDTO>> GetCarsByCategoryAsync(int categoryId)
        {
            return await _context.Cars
                .Where(c => (int)c.CarPurpose == categoryId)
                .Select(c => new CarByCategoryDTO
                {
                    Id = c.Id,
                    BrandName = c.BrandName,
                    Model = c.Model,
                    Year = c.Year,
                    RentalPricePerDay = c.RentalPricePerDay,
                    SalePrice = c.SalePrice,
                    Color = c.Color,
                    Description = c.Description,
                    CarPurpose = c.CarPurpose.ToString(),
                    LicensePlateNumber = c.LicensePlateNumber
                })
                .ToListAsync();
        }

        // Get Car By Office Id 
        public async Task<List<CarByOfficeDTO>> GetCarsByOfficeId(int officeId)
        {
            return await _context.Cars
                .Where(c => c.OfficeId == officeId && c.IsActive)
                .Include(c => c.CarImages) // ✅ تضمين الصور
                .Select(c => new CarByOfficeDTO
                {
                    Id = c.Id,
                    BrandName = c.BrandName,
                    Model = c.Model,
                    Year = c.Year,
                    Color = c.Color,
                    RentalPricePerDay = c.RentalPricePerDay,
                    SalePrice = c.SalePrice,
                    Description = c.Description,
                    CarPurpose = c.CarPurpose.ToString(),


                    ImageUrl = c.CarImages.Select(img => img.imageURL).FirstOrDefault()
                })
                .ToListAsync();
        }



        // Get car General Info 
        public async Task<CarGeneralInfoDTO> GetCarGeneralInfo(int carId)
        {
            var car = await _context.Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarGeneralInfoDTO
                {
                    Id = c.Id,
                    BrandName = c.BrandName,
                    LicensePlateNumber = c.LicensePlateNumber,
                    Model = c.Model,
                    Year = c.Year,
                    Color = c.Color,
                    RentalPricePerDay = c.RentalPricePerDay,
                    SalePrice = c.SalePrice,
                    Description = c.Description,
                    CarPurpose = c.CarPurpose.ToString()
                })
                .FirstOrDefaultAsync();

            return car;
        }


        //Get car Images 
        public async Task<List<CarImageDTO>> GetCarImages(int carId)
        {
            return await _context.CarImages
                .Where(i => i.CarId == carId)
                .Select(i => new CarImageDTO
                {
                    ImageURL = i.imageURL
                })
                .ToListAsync();
        }



        //Get Cat Specs
        public async Task<CarSpecsDTO> GetCarSpecs(int carId)
        {
            var specs = await _context.CarSpecs.FirstOrDefaultAsync(s => s.CarId == carId);
            if (specs == null)
                return null;

            return new CarSpecsDTO
            {
                EngineType = specs.EngineType,
                EngineCapacity = specs.EngineCapacity,
                HorsePower = specs.HorsePower,
                TransmissionType = specs.TransmissionType.ToString(),
                PerformanceScore = specs.performanceScore,
                FuelType = specs.FuelType.ToString(),
                SeatingCapacity = specs.SeatingCapacity
            };
        }



        // Get Car Average Price 
        public async Task<CarAveragePriceDTO> GetCarAveragePrice(int carId)
        {
            var carPrices = await _context.Cars
                .Where(c => c.Id == carId)
                .Select(c => new
                {
                    c.Id,
                    c.RentalPricePerDay,
                    c.SalePrice
                }).FirstOrDefaultAsync();

            if (carPrices == null)
                return null;

            return new CarAveragePriceDTO
            {
                CarId = carPrices.Id,
                AverageRentalPricePerDay = carPrices.RentalPricePerDay,
                AverageSalePrice = carPrices.SalePrice
            };
        }



        //Get Car Performance source 
        public async Task<CarPerformanceScoreDTO> GetCarPerformanceScore(int carId)
        {
            var specs = await _context.CarSpecs.FirstOrDefaultAsync(c => c.CarId == carId);
            if (specs == null)
                return null;

            return new CarPerformanceScoreDTO
            {
                CarId = carId,
                PerformanceScore = specs.performanceScore
            };
        }


        // Get Car For Sale 
        public async Task<List<CarGeneralInfoDTO>> GetCarsForSale()
        {
            return await _context.Cars
                .Where(c => c.CarPurpose == CarPurpose.ForSale && c.IsActive)
                .Select(c => new CarGeneralInfoDTO
                {
                    Id = c.Id,
                    BrandName = c.BrandName,
                    LicensePlateNumber = c.LicensePlateNumber,
                    Model = c.Model,
                    Year = c.Year,
                    Color = c.Color,
                    RentalPricePerDay = c.RentalPricePerDay,
                    SalePrice = c.SalePrice,
                    Description = c.Description,
                    CarPurpose = c.CarPurpose.ToString()
                })
                .ToListAsync();
        }



        // Check car Availability 
        public async Task<bool> CheckCarAvailability(int carId, DateTime startDate, DateTime endDate)
        {

            if (startDate >= endDate)
                throw new ArgumentException("StartDate must be earlier than EndDate");


            bool isBooked = await _context.Reservations.AnyAsync(r =>
                r.CarId == carId &&
               r.ReservationStatus == ReservationStatus.Approved &&
                (
                    (startDate >= r.StartDate && startDate < r.EndDate) ||
                    (endDate > r.StartDate && endDate <= r.EndDate) ||
                    (startDate <= r.StartDate && endDate >= r.EndDate)
                )
            );

            return !isBooked;
        }



        public async Task<List<CarReviewDTO>> GetCarReviews(int carId)
        {
            return await _context.CarReviews
                .Where(r => r.CarId == carId && r.ReviStatus == ReviewStatus.Approved)
                .Select(r => new CarReviewDTO
                {
                    ReviewTitle = r.ReviewTitle,
                    ReviewContent = r.ReviewContent,
                    StarsReview = (int)r.StarsReview,
                    UserName = r.User.FullName,
                    Date = r.CreationDate
                }).ToListAsync();
        }






        public async Task<List<CarByOfficeDTO>> GetCarsByCategoryAsync(OfficesCategory category, SortByOption sortBy, bool descending)
        {
            IQueryable<Car> query = _context.Cars
                .Where(c => c.Office.OfficeCategory == category && c.IsActive)
                .Include(c => c.CarReviews)
                .Include(c => c.CarImages);

            query = sortBy switch
            {
                SortByOption.Price => descending
                    ? query.OrderByDescending(c => c.RentalPricePerDay ?? c.SalePrice)
                    : query.OrderBy(c => c.RentalPricePerDay ?? c.SalePrice),

                SortByOption.Rating => descending
                    ? query.OrderByDescending(c => c.CarReviews.Average(r => (int?)r.StarsReview) ?? 0)
                    : query.OrderBy(c => c.CarReviews.Average(r => (int?)r.StarsReview) ?? 0),

                SortByOption.Date => descending
                    ? query.OrderByDescending(c => c.CreationDate)
                    : query.OrderBy(c => c.CreationDate),

                SortByOption.Model => descending
                    ? query.OrderByDescending(c => c.Model)
                    : query.OrderBy(c => c.Model),

                _ => query.OrderBy(c => c.Id)
            };

            return await query.Select(c => new CarByOfficeDTO
            {
                Id = c.Id,
                BrandName = c.BrandName,
                Model = c.Model,
                Year = c.Year,
                Color = c.Color,
                RentalPricePerDay = c.RentalPricePerDay,
                SalePrice = c.SalePrice,
                Description = c.Description,
                CarPurpose = c.CarPurpose.ToString(),
                ImageUrl = c.CarImages.Select(img => img.imageURL).FirstOrDefault()
            }).ToListAsync();
        }
    }

}

