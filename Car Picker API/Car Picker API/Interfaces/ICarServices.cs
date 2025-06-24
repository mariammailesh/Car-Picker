using Car_Picker_API.DTOs;
using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Interfaces
{
    public interface ICarServices
    {
        Task<List<CarByCategoryDTO>> GetCarsByCategoryAsync(int categoryId);

        Task<List<CarByOfficeDTO>> GetCarsByOfficeId(int officeId);
        Task<CarGeneralInfoDTO> GetCarGeneralInfo(int carId);

        Task<List<CarGeneralInfoDTO>> GetCarsForSale();

        Task<List<CarGeneralInfoDTO>> GetCarsForRent();

        Task<List<CarImageDTO>> GetCarImages(int carId);

        Task<CarSpecsDTO> GetCarSpecs(int carId);

        Task<CarAveragePriceDTO> GetCarAveragePrice(int carId);

        Task<CarPerformanceScoreDTO> GetCarPerformanceScore(int carId);


        Task<bool> CheckCarAvailability(int carId, DateTime startDate, DateTime endDate);
        Task<List<CarReviewDTO>> GetCarReviews(int carId);
        Task<List<CarByOfficeDTO>> GetCarsByCategoryAsync(OfficesCategory category, SortByOption sortBy, bool descending);



    }
}
