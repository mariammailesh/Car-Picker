﻿using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Entities
{
    public class User : ParentEntity
    {

        public string FullName { get; set; }
        public string Email { get; set; } // Unique email for the user
        public string Password { get; set; }//hashed
        public string PhoneNumber { get; set; }
        public bool IsLoggedIn { get; set; } = false;
        public string? OTPCode { get; set; }
        public DateTime? OTPExpiry { get; set; }
        public bool IsVerified { get; set; } = false;
        public DateOnly DateOfBirth { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string DrivingLicenseFrontImagePath { get; set; }
        public string DrivingLicenseBackImagePath { get; set; }
        public string NationalIDFrontImagePath { get; set; }
        public string NationalIDBackImagePath { get; set; }

        //Enum
        public Role RoleId { get; set; } = Role.Client; // Enum for Role stores 3
        public Gender Gender { get; set; }

        // Navigation properties
        public ICollection<Payment> Payments { get; set; } // Navigation property to Payments
        public ICollection<Reservation> Reservations { get; set; } // Navigation property to Reservations
        public ICollection<CarReview> CarReviews { get; set; } // Navigation property to CarReviews
        public ICollection<OfficeReview> OfficeReviews { get; set; } // Navigation property to OfficeReviews


    }
}
