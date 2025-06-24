using Car_Picker_API.DTOs;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly CarPickerDbContext _context;
        public OfficeService(CarPickerDbContext context)
        {


            _context = context;
        }



        public async Task<List<OfficeDTO>> GetAllOfficesAsync()
        {
            return await _context.Offices
                .Where(o => o.IsActive)
                .Select(o => new OfficeDTO
                {
                    Id = o.Id,
                    OfficeName = o.OfficeName,
                    ReservationsCount = o.ReservationsCount,
                    OfficeCategory = o.OfficeCategory.ToString(),
                    OfficeAddress = o.OfficeAddress,
                    OfficePhoneNumber = o.OfficePhoneNumber,
                    OfficeEmail = o.OfficeEmail,
                    OfficeDescription = o.OfficeDescription,
                    OfficeImageUrl = o.OfficeImageUrl
                })
                .ToListAsync();
        }





        public async Task<List<OfficeDTO>> GetOfficesByCategoryAsync(OfficesCategory category)
        {
            return await _context.Offices
                .Where(o => o.OfficeCategory == category && o.IsActive)
                .Select(o => new OfficeDTO
                {
                    Id = o.Id,
                    OfficeName = o.OfficeName,
                    OfficeAddress = o.OfficeAddress,
                    OfficePhoneNumber = o.OfficePhoneNumber,
                    OfficeEmail = o.OfficeEmail,
                    OfficeDescription = o.OfficeDescription,
                    ReservationsCount = o.ReservationsCount,
                    OfficeCategory = o.OfficeCategory.ToString(),
                    OfficeImageUrl = o.OfficeImageUrl
                }).ToListAsync();
        }





        public async Task<List<OfficeReviewDTO>> GetOfficeReviewsByOfficeIdAsync(int officeId)
        {
            var reviews = await _context.OfficeReviews
                .Include(r => r.User)
                .Where(r => r.OfficeId == officeId && r.ReviStatus == Helpers.Enums.ReviewStatus.Approved)
                .Select(r => new OfficeReviewDTO
                {
                    Id = r.Id,
                    ReviewTitle = r.ReviewTitle,
                    ReviewContent = r.ReviewContent,
                    ReviewStatus = r.ReviStatus.ToString(),
                    StarsReview = (int)r.StarsReview,
                    OfficeId = r.OfficeId,
                    UserId = r.UserId,
                    UserName = r.User.FullName
                })
                .ToListAsync();

            return reviews;
        }
    }


}
