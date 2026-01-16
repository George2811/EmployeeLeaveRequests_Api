using EmployeeLeaveRequests.Domain.Models.Constants;

namespace EmployeeLeaveRequests.Domain.Models
{
    public class LeaveRequest
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public Employee Employee { get; set; }

        public int GetDurationInDays => (EndDate - StartDate).Days + 1;
        public void Reject(string reason)
        {
            Status = LeaveStatus.REJECTED;
            Reason = reason;
        }
        public void Approve() => Status = LeaveStatus.APPROVED;
        public void PendingApproval() => Status = LeaveStatus.PENDING;
        public bool IsApproved() => Status == LeaveStatus.APPROVED;

    }
}
