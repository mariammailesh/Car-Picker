using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class Payment : ParentEntity
    {
        public DateTime PaymentDate { get; set; } // Date and time of the payment
        public float Amount { get; set; } // Amount of the payment
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending; // Status of the payment (Pending, Completed, Failed)
        public string? TransactionId { get; set; } // Unique identifier for the transaction
        public PaymentMethod PaymentMethod { get; set; }

        /// Foreign Key 
        public int ReservationId { get; set; }// Foreign Key to RentalReservation table
        public int UserId { get; set; } // Foreign Key to User table

        /// Navigation properties
        public Reservation Reservation { get; set; } // Navigation property to the Reservation entity
        public User User { get; set; } // Navigation property to the User entity


    }
}
