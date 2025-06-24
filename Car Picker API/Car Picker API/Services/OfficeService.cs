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
                    OfficeImageUrl = o.OfficeImageUrl,

                    AverageStarsReview = o.OfficeReviews.Any()
                        ? o.OfficeReviews.Average(r => (int)r.StarsReview)
                        : 0
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
                    
                    ReviewStatus = r.ReviStatus.ToString(),
                    StarsReview = r.StarsReview,
                    TotalReviewsForOffice = r.TotalReviewsForOffice ,
                    OfficeId = r.OfficeId,
                    UserId = r.UserId,
                    
                })
                .ToListAsync();

            return reviews;
        }
    }


}
