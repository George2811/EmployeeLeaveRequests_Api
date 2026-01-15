using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Persistence.Contexts;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveRequests.Persistence.Repository
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Employee _employee)
        {
            await _context.Employees.AddAsync(_employee);
        }

        public async Task<Employee> FindById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public void Remove(Employee _employee)
        {
            _context.Employees.Remove(_employee);
        }

        public void Update(Employee _employee)
        {
            _context.Employees.Update(_employee);
        }
    }
}
