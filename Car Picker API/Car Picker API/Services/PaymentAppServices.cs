using Car_Picker_API.DTOs;
using Car_Picker_API.Entities;
using Car_Picker_API.Helpers.Enums;
using Car_Picker_API.Interfaces;
using CarPicker_API.Context;
using Microsoft.EntityFrameworkCore;

namespace Car_Picker_API.Services
{
    public class PaymentAppServices : IPaymentInterface
    {
        private readonly CarPickerDbContext _context;
        public PaymentAppServices(CarPickerDbContext context)
        {
            _context = context;
        }
        public async Task<object> AddPaymentMethod(AddPaymentDTO input)
        {

            if (!Enum.TryParse<PaymentMethod>(input.PaymentMethod, true, out var parsedMethod))
            {
                return new { message = "Invalid payment method." };
            }

            var existing = await _context.Payments.FirstOrDefaultAsync(p =>
             p.PaymentMethod == parsedMethod && p.UserId == input.UserId);

            if (existing != null)
            {
                return new { message = "Payment method already exists for this user." };
            }

            var newPayment = new Payment
            {
                PaymentMethod = parsedMethod,
                UserId = input.UserId,
                PaymentStatus = Helpers.Enums.PaymentStatus.Pending,
                PaymentDate = DateTime.Now,
                Amount = 0,
                ReservationId = null,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreatedBy = "System",
                UpdatedBy = "System"
            };

            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();

            return new { message = "Payment method added successfully", paymentMethodId = newPayment.Id };
        }


        public async Task<List<GetAllPaymentsDTO>> GetAllPayments()
        {
            var payments = await _context.Payments.ToListAsync();

            var result = payments.Select(p => new GetAllPaymentsDTO
            {
                Id = p.Id,
                PaymentMethod = p.PaymentMethod.ToString(),
                Amount = p.Amount,
                PaymentStatus = p.PaymentStatus.ToString(),
                PaymentDate = p.PaymentDate,
                UserId = p.UserId
            }).ToList();

            return result;
        }
    }
}
