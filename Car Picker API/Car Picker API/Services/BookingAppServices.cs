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


            var reservation = new Reservation
            {
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                UserId = input.UserId,
                CarId = input.CarId,
                ReservationStatus = Helpers.Enums.ReservationStatus.Pending
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var payment = new Payment
            {
                PaymentDate = DateTime.Now,
                Amount = 0,
                PaymentStatus = Helpers.Enums.PaymentStatus.Pending,
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
                paymentId = payment.Id
            };
        }

        public async Task<List<GetMyBookingsDTO>> GetMyBookings(int userId)
        {
            var reservations = await _context.Reservations
            .Include(r => r.Car)
            .Where(r => r.UserId == userId)
            .ToListAsync();

            return reservations.Select(r => new GetMyBookingsDTO
            {
                UserId = r.UserId,
                CarId = r.CarId,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                IsDeliveredCar = r.IsDeliveredCar,
                Model = r.Car.Model,
                ReservationStatus = r.ReservationStatus.ToString()
            }).ToList();

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
                StartDate = r.StartDate,
                EndDate = r.EndDate,
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

        public async Task<bool> DeleteBooking(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}