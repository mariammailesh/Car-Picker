namespace Car_Picker_API.DTOs
{
    public class CarByOfficeForSaleDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        public string Year { get; set; }

        public float? SalePrice { get; set; }



        public string ImageUrl { get; set; }

    }
}
