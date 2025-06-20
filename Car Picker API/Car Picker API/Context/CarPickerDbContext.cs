using Car_Picker_API.Entities;
using Microsoft.EntityFrameworkCore;



namespace CarPicker_API.Context
{
    public class CarPickerDbContext : DbContext
    {
        //listing the tables in this database here!
       
        public DbSet<User> Users { get; set; } // Table for Users


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
