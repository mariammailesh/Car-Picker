using Car_Picker_API.DTOs;
using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Interfaces
{
    public interface IOfficeService
    {
        Task<List<OfficeDTO>> GetAllOfficesAsync();
        Task<List<OfficeDTO>> GetOfficesByCategoryAsync(OfficesCategory category);
<<<<<<< Updated upstream

        Task<List<GetOfficeInfoDTO>> GetOfficesInfo();


=======
       Task<List<GetOfficeInfoDTO>> GetOfficesInfo();
>>>>>>> Stashed changes
    }
}
