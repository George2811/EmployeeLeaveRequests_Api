namespace EmployeeLeaveRequests.Domain.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Employee"; // "Employee" o "Manager"
    }
}
