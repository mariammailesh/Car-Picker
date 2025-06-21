using Car_Picker_API.DTOs;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class LookupAppService : ILookupInterface
    {
        private readonly CarPickerDbContext _context;
        public LookupAppService(CarPickerDbContext context)
        {

            _context = context;
        }
        public async Task<List<LookupItemDTO>> GetLookupItemsByTypeId(int typeId)
        {
            var query = from item in _context.LookupItems
                        join type in _context.LookupTypes on item.LookupTypeId equals type.Id
                        where type.Id == typeId
                        select new LookupItemDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            ParentName = type.Name
                        };
            return await query.ToListAsync();
        }
    }
}
