using EmployeeLeaveRequests.Domain.Models;

namespace EmployeeLeaveRequests.Domain.Services.Communications
{
    public class EmployeeResponse : BaseResponse<Employee>
    {
        public EmployeeResponse(Employee resource) : base(resource)
        {
        }

        public EmployeeResponse(string message) : base(message)
        {
        }
    }
}
