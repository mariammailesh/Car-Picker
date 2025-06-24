using Car_Picker_API.DTOs;

namespace Car_Picker_API.Interfaces
{
    public interface ICategoriesInterface
    {
        Task<List<GetAllCategoriesDTO>> GetAllCategories();
    }
}
