using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class Car:ParentEntity
    {
        public string BrandName { get; set; } // nvarchar(max) not null
        public string LicensePlateNumber { get; set; } // nvarchar(max) not null
        public string Model { get; set; } // nvarchar(max) not null
        public string Year { get; set; } // nvarchar(max) not null
        public string Color { get; set; } // nvarchar(max) not null
        public float? RentalPricePerDay { get; set; } // float null
        public float? InsuranceFeesPerDay { get; set; } // float nullable
        public float CurrentMileage { get; set; } // float not null
        public float? SalePrice { get; set; } // float nullable
        public CarPurpose CarPurpose { get; set; } // Enum for CarPurpose
        public string? Description { get; set; } // nvarchar(max) nullable

        // Foreign Keys
        public int OfficeId { get; set; } // Foreign Key to Office table


        // Navigation properties
        public ICollection<CarReview> CarReviews { get; set; } // Navigation property to CarReviews
        public ICollection<Reservation> Reservations { get; set; } // Navigation property to Reservations
        public CarSpecs CarSpecs { get; set; } // Navigation property to CarSpecs entity
        public ICollection<CarImage> CarImages { get; set; } // Navigation property to CarImages
        public Office Office { get; set; } // Navigation property to Office entity

    }
}
