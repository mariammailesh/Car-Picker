using Car_Picker_API.DTOs;
using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Interfaces
{
    public interface IOfficeService
    {
        Task<List<OfficeDTO>> GetAllOfficesAsync();

        Task<List<GetOfficeInfoDTO>> GetOfficesInfo();


        Task<List<GetOfficeInfoDTO>> GetOfficesInfo();
    }
}
