namespace EmployeeLeaveRequests.Domain.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
