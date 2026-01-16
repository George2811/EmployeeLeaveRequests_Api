using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Models.Constants;
using EmployeeLeaveRequests.Domain.Persistence.Contexts;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveRequests.Persistence.Repository
{
    public class LeaveRequestRepository : BaseRepository, ILeaveRequestRepository
    {
        public LeaveRequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(LeaveRequest _leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(_leaveRequest);
        }

        public async Task<LeaveRequest> FindById(Guid id)
        {
            return await _context.LeaveRequests.FindAsync(id);
        }

        public async Task<IEnumerable<LeaveRequest>> ListAsync()
        {
            return await _context.LeaveRequests.ToListAsync();
        }

        public void Remove(LeaveRequest _leaveRequest)
        {
            _context.LeaveRequests.Remove(_leaveRequest);
        }

        public void Update(LeaveRequest _leaveRequest)
        {
            _context.LeaveRequests.Update(_leaveRequest);
        }

        public async Task<bool> HasApprovedOverlappingLeave(Guid _employeeId, DateTime _startDate, DateTime _endDate)
        {
            return await _context.LeaveRequests.AnyAsync(lr =>
                lr.EmployeeId == _employeeId &&
                lr.Status == LeaveStatus.APPROVED &&
                lr.StartDate <= _endDate &&
                lr.EndDate >= _startDate
            );
        }

        public async Task<IEnumerable<LeaveRequest>> ListByEmployeeIdAsync(Guid _employeeId)
        {
            return await _context.LeaveRequests.Where(lr => lr.EmployeeId == _employeeId).ToListAsync();
        }
    }
}
