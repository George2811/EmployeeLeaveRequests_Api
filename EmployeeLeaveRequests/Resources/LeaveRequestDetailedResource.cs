namespace EmployeeLeaveRequests.Resources
{
    public class LeaveRequestDetailedResource
    {
        public Guid Id { get; set; }
        public EmployeeResource Employee { get; init; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
