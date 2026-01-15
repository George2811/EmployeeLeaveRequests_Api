using EmployeeLeaveRequests.Domain.Persistence.Contexts;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;

namespace EmployeeLeaveRequests.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
