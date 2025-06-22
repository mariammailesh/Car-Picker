namespace Car_Picker_API.DTOs
{
    public class GetAllPaymentsDTO
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public float Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public int UserId { get; set; }
    }
}
