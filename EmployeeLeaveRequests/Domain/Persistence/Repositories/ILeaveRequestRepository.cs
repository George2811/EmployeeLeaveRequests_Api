using EmployeeLeaveRequests.Domain.Models;

namespace EmployeeLeaveRequests.Domain.Persistence.Repositories
{
    public interface ILeaveRequestRepository
    {
        Task<IEnumerable<LeaveRequest>> ListAsync();
        Task<IEnumerable<LeaveRequest>> ListByEmployeeIdAsync(Guid _employeeId);
        Task<LeaveRequest> FindById(Guid id);
        Task AddAsync(LeaveRequest _leaveRequest);
        void Update(LeaveRequest _leaveRequest);
        void Remove(LeaveRequest _leaveRequest);

        Task<bool> HasApprovedOverlappingLeave(Guid _employeeId, DateTime _startDate, DateTime _endDate);
    }
}
