namespace Car_Picker_API.DTOs
{
    public class OfficeDTO
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public int ReservationsCount { get; set; }
        public string OfficeCategory { get; set; } // Enum كـ string
        public string OfficeAddress { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string OfficeEmail { get; set; }
        public string OfficeDescription { get; set; }

        public string OfficeImageUrl { get; set; }


    }
}
