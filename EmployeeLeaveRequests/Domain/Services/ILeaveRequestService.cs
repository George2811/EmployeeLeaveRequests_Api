using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Services.Communications;

namespace EmployeeLeaveRequests.Domain.Services
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequest>> ListAsync(Guid _employeeId);
        Task<LeaveRequestResponse> FindAsyncById(Guid id);
        Task<LeaveRequestResponse> SaveAsync(LeaveRequest _leaveRequest);
        Task<LeaveRequestResponse> ApproveAsync(Guid leaveRequestId, Employee _employee);
        Task<LeaveRequestResponse> CancelAsync(Guid leaveRequestId, Guid employeeId);
    }
}
