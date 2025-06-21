namespace Car_Picker_API.Entities
{
    public class OfficeImage : ParentEntity
    {
        public string ImagePath { get; set; } // Path/URL of the office image


        //Foreign Key
        public int OfficeId { get; set; } // Foreign Key to Office entity

        // Navigation property
        public Office Office { get; set; } // Navigation property to the Office entity

    }
}
