namespace Car_Picker_API.DTOs
{
    public class CarFilterDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
       
        public float? SalePrice { get; set; }

        public string CarPurpose { get; set; } 
        public string? ImageUrl { get; set; }


    }
}
