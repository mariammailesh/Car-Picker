namespace Car_Picker_API.Entities
{
    public class CarImage : ParentEntity
    {
        public string imageURL { get; set; } // nvarchar(max) not null
        public int CarId { get; set; } // Foreign Key to Car table
    }
}
