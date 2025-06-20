namespace Car_Picker_API.Entities
{
    public class User:ParentEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } //hashed

        public string Password { get; set; } //hased 
        public string Email { get; set; } //hashed
        public string? OTPCode { get; set; }
        public DateTime? OTPExipry { get; set; }
        public int RoleId { get; set; }
        public bool IsVerfied { get; set; } = false;
        public bool? IsLoggedIn { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
