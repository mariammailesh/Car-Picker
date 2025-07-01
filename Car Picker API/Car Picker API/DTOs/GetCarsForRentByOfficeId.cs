namespace Car_Picker_API.DTOs
{
    public class GetCarsForRentByOfficeId
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        public float? RentalPricePerDay { get; set; }

        public string Year { get; set; }
        public string? ImageUrl { get; set; }
    }
}
