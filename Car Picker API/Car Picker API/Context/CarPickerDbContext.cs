using Car_Picker_API.Entities;
using Microsoft.EntityFrameworkCore;



namespace CarPicker_API.Context
{
    public class CarPickerDbContext : DbContext
    {
        //listing the tables in this database here!
       
        public DbSet<User> Users { get; set; } // Table for Users
        public DbSet<Car> Cars { get; set; } // Table for Cars
        public DbSet<CarImage> CarImages { get; set; } // Table for Car Images
        public DbSet<CarReview> CarReviews { get; set; } // Table for Car Reviews
        public DbSet<CarSpecs> CarSpecs { get; set; } // Table for Car Specifications
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

            // ضبط علاقة Reservation مع Office مع منع الحذف التلقائي (No Cascade)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Office)
                .WithMany(o => o.Reservations)
                .HasForeignKey(r => r.OfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ضبط علاقة Reservation مع Car - ممكن تستخدم Cascade أو Restrict حسب حاجتك
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // ضبط علاقة Reservation مع User
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ضبط علاقة Payment مع Reservation
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
