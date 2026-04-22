namespace StoreApp.Application.DTOs
{
    public class StaffDto
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Active { get; set; }
        public int StoreId { get; set; }
        public int ManagerId { get; set; }
    }
}
