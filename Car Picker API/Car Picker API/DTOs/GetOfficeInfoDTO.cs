namespace Car_Picker_API.DTOs
{
    public class GetOfficeInfoDTO
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public int ReservationsCount { get; set; }
        public string OfficeCategory { get; set; } // Enum as string
        public string OfficeAddress { get; set; }
        public string OfficePhoneNumber { get; set; }
        public double AverageStarsReview { get; set; }
        public string OfficeDescription { get; set; }
        public string OfficeImageUrl { get; set; } // Single image

    }
}
