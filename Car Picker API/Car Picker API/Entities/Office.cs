using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class Office : ParentEntity
    {
        public string OfficeName { get; set; }
        public int ReservationsCount { get; set; } // Number of reservations made at this office
        public OfficesCategory OfficeCategory { get; set; } //enum
        public string OfficeAddress { get; set; } // Address of the office
        public string OfficePhoneNumber { get; set; } // Phone number of the office
        public string OfficeEmail { get; set; } // Email address of the office
        public string OfficeDescription { get; set; } // Description of the office
        public string OfficeImageUrl { get; set; }


        // Navigation properties
        public ICollection<Car> Cars { get; set; } // Navigation property to Cars
        public ICollection<Reservation> Reservations { get; set; } // Navigation property to Reservations
        public ICollection<OfficeImage> OfficeImages { get; set; } // Navigation property to OfficeImages
        public ICollection<OfficeReview> OfficeReviews { get; set; } // Navigation property to OfficeReviews



    }
}
