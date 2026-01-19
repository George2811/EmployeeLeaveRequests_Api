using EmployeeLeaveRequests.Domain.Models;

namespace EmployeeLeaveRequests.Services.Auth
{
    public interface IJwtTokenService
    {
        string GenerateToken(Employee employee);
    }
}
