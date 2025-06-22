namespace Car_Picker_API.DTOs
{
    public class GetMyBookingsDTO
    {
        public int UserId { get; set; }

        public int CarId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsDeliveredCar { get; set; }

        public string Model { get; set; }

        public string ReservationStatus { get; set; }
    }
}
