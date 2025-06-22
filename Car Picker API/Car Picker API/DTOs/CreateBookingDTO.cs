using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.DTOs
{
    public class CreateBookingDTO
    {
        public int CarId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; } // enum

    }
}
