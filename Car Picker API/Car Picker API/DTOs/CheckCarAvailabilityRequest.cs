namespace Car_Picker_API.DTOs
{
    public class CheckCarAvailabilityRequest
    {
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
