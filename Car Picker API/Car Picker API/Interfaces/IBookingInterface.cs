using Car_Picker_API.DTOs;
using Car_Picker_API.Helpers.Enums;

namespace Car_Picker_API.Interfaces
{
    public interface IBookingInterface
    {
        Task<object> CreateBooking(CreateBookingDTO input);

        Task<List<GetMyBookingsDTO>> GetMyBookings(int userId);

        Task<List<GetBookingRequestDTO>> GetAllBookingRequests();

        Task<bool> UpdateBookingStatus(int reservationId, ReservationStatus newStatus);

    }
}
