using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Models.Constants;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using EmployeeLeaveRequests.Domain.Services;
using EmployeeLeaveRequests.Domain.Services.Communications;

namespace EmployeeLeaveRequests.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<LeaveRequestResponse> UpdateStatusAsync(Guid userId, LeaveRequest _leaveRequest)
        {
            var employee = await _employeeRepository.FindById(userId);

            if (employee == null) return new LeaveRequestResponse($"Employee with Id {userId} does not exists.");
            if (employee.Role != Role.MANAGER) return new LeaveRequestResponse($"User {employee.Email} does not have the permission to update status.");

            var leaveRequest = await _leaveRequestRepository.FindById(_leaveRequest.Id);
            if (leaveRequest == null) return new LeaveRequestResponse($"Leave request with Id {_leaveRequest.Id} does not exists.");

            // Pide Aprobar pero ya está aprobada
            if(_leaveRequest.IsApproved() && leaveRequest.IsApproved()) return new LeaveRequestResponse($"Leave request has already been approved.");

            // Pide Rechazar pero ya está rechazada
            if (_leaveRequest.IsRejected() && leaveRequest.IsRejected()) return new LeaveRequestResponse($"Leave request has already been rejected.");

            var hasApprovedOverlappingLeave = await _leaveRequestRepository.HasApprovedOverlappingLeave(_leaveRequest.EmployeeId, leaveRequest.StartDate, leaveRequest.EndDate);

            if(_leaveRequest.IsApproved() && hasApprovedOverlappingLeave) return new LeaveRequestResponse($"No overlap is allowed: An approved leave request already exists between those dates.");

            switch (_leaveRequest.Status)
            {
                case LeaveStatus.APPROVED:
                    leaveRequest.Approve();
                    break;
                case LeaveStatus.REJECTED:
                    leaveRequest.Reject();
                    break;
                default:
                    break;
            }

            try
            {
                _leaveRequestRepository.Update(leaveRequest);
                await _unitOfWork.CompleteAsync();

                return new LeaveRequestResponse(leaveRequest);
            }
            catch (Exception ex)
            {
                return new LeaveRequestResponse($"An error ocurred while saving the Leave Request: {ex.Message}");
            }
        }

        public async Task<LeaveRequestResponse> CancelAsync(Guid leaveRequestId, Guid userId)
        {
            var leaveRequest = await _leaveRequestRepository.FindById(leaveRequestId);
            if (leaveRequest == null) return new LeaveRequestResponse($"Leave request with Id {leaveRequestId} does not exists.");
            if(leaveRequest.EmployeeId != userId) return new LeaveRequestResponse($"User with Id {userId} does not have the permission to cancel.");

            try
            {
                _leaveRequestRepository.Remove(leaveRequest);
                await _unitOfWork.CompleteAsync();
                return new LeaveRequestResponse(leaveRequest);
            }
            catch (Exception ex)
            {
                return new LeaveRequestResponse($"An error ocurred while canceling the Leave Request: {ex.Message}");
            }

        }

        public async Task<LeaveRequestResponse> FindAsyncById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LeaveRequest>> ListAsync(Guid _employeeId)
        {
            var _employee = await _employeeRepository.FindById(_employeeId);

            if (_employee.Role == Role.MANAGER)
            {
                return await _leaveRequestRepository.ListAsync();
            }

            return await _leaveRequestRepository.ListByEmployeeIdAsync(_employee.Id);
        }

        public async Task<LeaveRequestResponse> SaveAsync(LeaveRequest _leaveRequest)
        {
            int days = _leaveRequest.GetDurationInDays;

            if (days > 15)
            {
                _leaveRequest.Reject();
            }
            else
            {
                _leaveRequest.PendingApproval();
            }

            try
            {
                await _leaveRequestRepository.AddAsync(_leaveRequest);
                await _unitOfWork.CompleteAsync();

                return new LeaveRequestResponse(_leaveRequest);
            }
            catch (Exception ex)
            {
                return new LeaveRequestResponse($"An error ocurred while saving the Leave Request: {ex.Message}");
            }
        }

    }
}
