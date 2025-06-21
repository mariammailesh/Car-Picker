namespace Car_Picker_API.Entities
{
    public abstract class ParentEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }  // declared only once
        public string? UpdatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
