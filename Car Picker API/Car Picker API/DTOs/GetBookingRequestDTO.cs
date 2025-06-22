namespace Car_Picker_API.DTOs
{
    public class GetBookingRequestDTO
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public string CarModel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReservationStatus { get; set; }
        public bool IsDeliveredCar { get; set; }
       
    }
}
