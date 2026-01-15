using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using EmployeeLeaveRequests.Domain.Services;
using EmployeeLeaveRequests.Domain.Services.Communications;

namespace EmployeeLeaveRequests.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<EmployeeResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> FindAsyncById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> SaveAsync(Employee _employee)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> UpdateAsync(Guid id, Employee _employee)
        {
            throw new NotImplementedException();
        }
    }
}
