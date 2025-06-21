namespace Car_Picker_API.Entities
{
    public class LookupType:ParentEntity
    {
        public string Name { get; set; } // Name of the lookup type

        // Navigation property
        public ICollection<LookupItem> LookupItems { get; set; } // Collection of LookupItem entities associated with this LookupType
    }
}
