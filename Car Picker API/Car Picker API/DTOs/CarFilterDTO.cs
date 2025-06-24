namespace Car_Picker_API.DTOs
{
    public class CarFilterDTO
    {
        public int? OfficeId { get; set; }
        public string? Brand { get; set; }
        public string? FuelType { get; set; }
        public string? TransmissionType { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }

        // Sorting options: "price", "rating", "date", "model"
        public string? SortBy { get; set; }
        public bool Descending { get; set; } = false;


    }
}
