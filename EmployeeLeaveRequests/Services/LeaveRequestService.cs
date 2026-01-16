using Azure.Core;
using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Models.Constants;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using EmployeeLeaveRequests.Domain.Services;
using EmployeeLeaveRequests.Domain.Services.Communications;
using Microsoft.VisualBasic;

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

        public async Task<LeaveRequestResponse> ApproveAsync(Guid leaveRequestId, Employee _employee)
        {
            var employee = await _employeeRepository.FindById(_employee.Id);

            if (employee == null) return new LeaveRequestResponse($"Employee with Id {_employee.Id} does not exists.");
            if (employee.Role != Role.MANAGER) return new LeaveRequestResponse($"User {employee.Email} does not have the permission to approve.");

            var leaveRequest = await _leaveRequestRepository.FindById(leaveRequestId);
            if (leaveRequest == null) return new LeaveRequestResponse($"Leave request with Id {employee.Id} does not exists.");

            leaveRequest.Approve();

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

        public async Task<LeaveRequestResponse> CancelAsync(Guid leaveRequestId, Guid employeeId)
        {
            var leaveRequest = await _leaveRequestRepository.FindById(leaveRequestId);
            if (leaveRequest == null) return new LeaveRequestResponse($"Leave request with Id {leaveRequestId} does not exists.");
            if(leaveRequest.EmployeeId != employeeId) return new LeaveRequestResponse($"User with Id {employeeId} does not have the permission to cancel.");

            try
            {
                _leaveRequestRepository.Remove(leaveRequest);
                await _unitOfWork.CompleteAsync();
                return new LeaveRequestResponse($"Leave Request with Id {leaveRequestId} was removed successfully.");
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
                _leaveRequest.Reject("Exceeds 15 consecutive days. Manager override required.");
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
