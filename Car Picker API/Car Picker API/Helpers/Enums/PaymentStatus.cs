namespace Car_Picker_API.Helpers.Enums
{
    public enum PaymentStatus
    {
        Pending = 1, // Payment is pending
        Completed = 2, // Payment has been completed successfully
        Failed = 3, // Payment has failed
        Refunded = 4, // Payment has been refunded
        Cancelled = 5 // Payment has been cancelled
    }
}
