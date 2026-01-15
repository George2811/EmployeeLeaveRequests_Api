using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Services.Communications;

namespace EmployeeLeaveRequests.Domain.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<EmployeeResponse> FindAsyncById(Guid id);
        Task<EmployeeResponse> SaveAsync(Employee _employee);
        Task<EmployeeResponse> UpdateAsync(Guid id, Employee _employee);
        Task<EmployeeResponse> DeleteAsync(Guid id);
    }
}
