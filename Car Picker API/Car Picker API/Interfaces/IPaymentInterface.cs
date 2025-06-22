using Car_Picker_API.DTOs;

namespace Car_Picker_API.Interfaces
{
    public interface IPaymentInterface
    {
        Task<object> AddPaymentMethod(AddPaymentDTO input);
        Task<List<GetAllPaymentsDTO>> GetAllPayments();
    }
}
