using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class Reservation:ParentEntity
    {
        public DateTime StartDate { get; set; } // Start date of the reservation
        public DateTime EndDate { get; set; } // End date of the reservation
        public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Pending; // Status of the reservation (Pending, Approved, Rejected)
        public bool IsDeliveredCar { get; set; } = false; // Indicates if the car should be delivered to a specific pickup location
        public string? PickupLocation { get; set; } // Location where the car will be picked up, if applicable
        public string? DropoffLocation { get; set; } // Location where the car will be dropped off, if applicable
        public int UserId { get; set; }
        public int CarId { get; set; }
        
    }
}
