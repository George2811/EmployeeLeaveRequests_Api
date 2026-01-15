using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using EmployeeLeaveRequests.Domain.Services;
using EmployeeLeaveRequests.Domain.Services.Communications;

namespace EmployeeLeaveRequests.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IUnitOfWork unitOfWork)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<LeaveRequestResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequestResponse> FindAsyncById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveRequest>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequestResponse> SaveAsync(LeaveRequest _leaveRequest)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequestResponse> UpdateAsync(Guid id, LeaveRequest _leaveRequest)
        {
            throw new NotImplementedException();
        }
    }
}
