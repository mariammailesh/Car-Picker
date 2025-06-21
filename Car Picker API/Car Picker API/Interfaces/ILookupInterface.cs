using Car_Picker_API.DTOs;
using Car_Picker_API.Services;

namespace Car_Picker_API.Interfaces
{
    public interface ILookupInterface
    {
        Task<List<LookupItemDTO>> GetLookupItemsByTypeId(int typeId);

    }
}
