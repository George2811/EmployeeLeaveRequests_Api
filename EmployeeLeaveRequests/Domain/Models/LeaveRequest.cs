namespace EmployeeLeaveRequests.Domain.Models
{
    public class LeaveRequest
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = LeaveStatus.PENDING;
        public string Reason { get; set; } = string.Empty;

        // Propiedad calculada para la regla de los 15 días
        public int DurationInDays => (EndDate - StartDate).Days + 1;
    }
}
