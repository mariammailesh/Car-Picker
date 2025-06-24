namespace Car_Picker_API.DTOs
{
    public class CarAveragePriceDTO
    {
        public int CarId { get; set; }
        public float? AverageRentalPricePerDay { get; set; }
        public float? AverageSalePrice { get; set; } 

    }
}
