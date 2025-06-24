namespace Car_Picker_API.DTOs
{
    public class CarByOfficeDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public float? RentalPricePerDay { get; set; }
        public float? SalePrice { get; set; }
        public string Description { get; set; }
        public string CarPurpose { get; set; }

        public string ImageUrl { get; set; }

    }
}
