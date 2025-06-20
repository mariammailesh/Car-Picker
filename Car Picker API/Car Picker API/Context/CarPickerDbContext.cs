using Car_Picker_API.Entities;
using Microsoft.EntityFrameworkCore;



namespace CarPicker_API.Context
{
    public class CarPickerDbContext : DbContext
    {
        //listing the tables in this database here!
       
        public DbSet<User> Users { get; set; } // Table for Users
        public DbSet<ParentEntity> ParentEntities { get; set; } // Table for Parent Entities
        public DbSet<Car> Cars { get; set; } // Table for Cars
        public DbSet<CarImage> CarImages { get; set; } // Table for Car Images
        public DbSet<CarReview> CarReviews { get; set; } // Table for Car Reviews
        public DbSet<CarSpecs> CarSpecs { get; set; } // Table for Car Specifications
        public DbSet<LookupItem> LookupItems { get; set; } // Table for Lookup Items
        public DbSet<LookupType> LookupTypes { get; set; } // Table for Lookup Types
        public DbSet<Office> Offices { get; set; } // Table for Offices
        public DbSet<OfficeReview> OfficeReviews { get; set; } // Table for Office Reviews
        public DbSet<OfficeImage> OfficeImages { get; set; } // Table for Office Images
        public DbSet<Payment> Payments { get; set; } // Table for Payments
        public DbSet<Reservation> Reservations { get; set; } // Table for Reservations

        //the rest of the code
        public CarPickerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected CarPickerDbContext()
        {
        }

        //override to call the on model creating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional model configurations here
        }
    }
}
