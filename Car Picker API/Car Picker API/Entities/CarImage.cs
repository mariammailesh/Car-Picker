namespace Car_Picker_API.Entities
{
    public class CarImage : ParentEntity
    {
        public string imageURL { get; set; } // nvarchar(max) not null

        // Foreign Key
        public int CarId { get; set; } // Foreign Key to Car table
       
        
        // Navigation property
        public Car Car { get; set; } // Navigation property to the Car entity

    }
}
