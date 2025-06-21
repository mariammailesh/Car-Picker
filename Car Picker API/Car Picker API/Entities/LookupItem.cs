namespace Car_Picker_API.Entities
{
    public class LookupItem:ParentEntity
    {
        public string Name { get; set; }
        public int LookupTypeId { get; set; }

        // Navigation property
        public LookupType LookupType { get; set; } // Navigation property to LookupType entity
    }
}
