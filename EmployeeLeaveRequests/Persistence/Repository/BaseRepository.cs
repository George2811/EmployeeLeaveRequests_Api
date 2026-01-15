using EmployeeLeaveRequests.Domain.Persistence.Contexts;

namespace EmployeeLeaveRequests.Persistence.Repository
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
