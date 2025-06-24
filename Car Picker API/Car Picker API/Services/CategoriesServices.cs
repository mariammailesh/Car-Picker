using Car_Picker_API.DTOs;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class CategoriesServices : ICategoriesInterface
    {
        private readonly CarPickerDbContext _context;

        public CategoriesServices(CarPickerDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllCategoriesDTO>> GetAllCategories()
        {
            var categories = await _context.Offices.Where(c => c.IsActive == true)
           .Select(c => new GetAllCategoriesDTO
           {
               Id = c.Id,
               OfficeName = c.OfficeName,
               ReservationsCount = c.ReservationsCount,
               OfficeCategory = c.OfficeCategory.ToString(),
               OfficeAddress = c.OfficeAddress,
               OfficePhoneNumber = c.OfficePhoneNumber,
               OfficeEmail = c.OfficeEmail,
               OfficeDescription = c.OfficeDescription,
               OfficeImageUrl = c.OfficeImageUrl
           }).ToListAsync();

            return categories;

        }
    }
}
