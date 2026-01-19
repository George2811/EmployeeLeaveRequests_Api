using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Models.Constants;
using EmployeeLeaveRequests.Domain.Persistence.Contexts;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using EmployeeLeaveRequests.Resources;
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
            return await _context.LeaveRequests
            .AsNoTracking()
            .Join(
                _context.Employees,
                lr => lr.EmployeeId,
                e => e.Id,
                (lr, e) => new LeaveRequest
                {
                    Id = lr.Id,
                    StartDate = lr.StartDate,
                    EndDate = lr.EndDate,
                    Status = lr.Status.ToString(),
                    Reason = lr.Reason,
                    Employee = new Employee
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Email = e.Email,
                        Role = e.Role
                    }
                }
            )
            .ToListAsync();
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
