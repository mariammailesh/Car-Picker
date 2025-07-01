using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs
{
    public class OfficeDTO
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public int ReservationsCount { get; set; }
        public string OfficeCategory { get; set; } // Enum كـ string
       

        public double AverageStarsReview { get; set; }
        

        public string OfficeImageUrl { get; set; }


    }
}
