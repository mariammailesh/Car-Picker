using Car_Picker_API.DTOs;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class BookingAppServices : IBookingInterface
    {
        private readonly CarPickerDbContext _context;
        public BookingAppServices(CarPickerDbContext context)
        {
            _context = context;
        }
        public async Task<object> CreateBooking(CreateBookingDTO input)
        {

            if (input == null)
                throw new ArgumentNullException(nameof(input), "Booking details cannot be null.");

            var car = await _context.Cars.FindAsync(input.CarId);
            if (car == null)
                throw new Exception("Car not found");

            if (input.TotalDays <= 0)
                throw new Exception("Total days must be greater than zero.");

         
            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(input.TotalDays);

            var reservation = new Reservation
            {
                StartDate = startDate,
                EndDate = endDate,
                UserId = input.UserId,
                CarId = input.CarId,
                OfficeId = car.OfficeId,
                CreatedBy = "System",
                UpdatedBy = "System",
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ReservationStatus = ReservationStatus.Pending
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var payment = new Payment
            {
                PaymentDate = DateTime.Now,
                Amount = 0,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = input.PaymentMethod,
                ReservationId = reservation.Id,
                UserId = input.UserId,
                CreatedBy = "System",
                UpdatedBy = "System",
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new
            {
                message = "Booking and payment created successfully",
                reservationId = reservation.Id,
                paymentId = payment.Id,
                startDate = startDate,
                endDate = endDate
            };
        }

        public async Task<List<GetMyBookingsDTO>> GetMyBookings(int userId)
        {
            var reservations = await _context.Reservations
         .Include(r => r.Car)
         .Where(r => r.UserId == userId)
         .ToListAsync();

            var filtered = reservations
                .Where(r => r.Car != null && r.Car.IsActive)
                .Select(r => new GetMyBookingsDTO
                {
                    UserId = r.UserId,
                    CarId = r.CarId,
                    TotalDays = (r.EndDate - r.StartDate).Days,
                    IsDeliveredCar = r.IsDeliveredCar,
                    Model = r.Car.Model,
                    ReservationStatus = r.ReservationStatus.ToString()
                })
                .ToList();

            return filtered;

        }


        public async Task<List<GetBookingRequestDTO>> GetAllBookingRequests()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.User)
                .ToListAsync();

            return reservations.Select(r => new GetBookingRequestDTO
            {
                ReservationId = r.Id,
                UserId = r.UserId,
                CarId = r.CarId,
                CarModel = r.Car.Model,
                TotalDays = (r.EndDate - r.StartDate).Days,
                ReservationStatus = r.ReservationStatus.ToString(),
                IsDeliveredCar = r.IsDeliveredCar,
            }).ToList();
        }

        public async Task<bool> UpdateBookingStatus(int reservationId, ReservationStatus newStatus)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
                throw new KeyNotFoundException("Reservation not found.");

            reservation.ReservationStatus = newStatus;
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();

            return true;
        }

       
    }
}