namespace Car_Picker_API.Entities
{
    public class OfficeImage:ParentEntity
    {
        public int OfficeId { get; set; } // Foreign Key to Office entity
        public string ImagePath { get; set; } // Path/URL of the office image
    }
}
