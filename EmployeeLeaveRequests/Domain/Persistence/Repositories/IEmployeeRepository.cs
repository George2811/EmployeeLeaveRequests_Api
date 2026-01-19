using EmployeeLeaveRequests.Domain.Models;

namespace EmployeeLeaveRequests.Domain.Persistence.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<Employee> FindById(Guid id);
        Task AddAsync(Employee _employee);
        void Update(Employee _employee);
        void Remove(Employee _employee);
        Task<Employee> GetByEmailAsync(string email);
    }
}
