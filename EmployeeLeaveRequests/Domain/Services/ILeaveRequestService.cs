using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Services.Communications;

namespace EmployeeLeaveRequests.Domain.Services
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequest>> ListAsync();
        Task<LeaveRequestResponse> FindAsyncById(Guid id);
        Task<LeaveRequestResponse> SaveAsync(LeaveRequest _leaveRequest);
        Task<LeaveRequestResponse> UpdateAsync(Guid id, LeaveRequest _leaveRequest);
        Task<LeaveRequestResponse> DeleteAsync(Guid id);
    }
}
