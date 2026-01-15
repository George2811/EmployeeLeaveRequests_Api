using EmployeeLeaveRequests.Domain.Models;

namespace EmployeeLeaveRequests.Domain.Services.Communications
{
    public class LeaveRequestResponse : BaseResponse<LeaveRequest>
    {
        public LeaveRequestResponse(LeaveRequest resource) : base(resource)
        {
        }

        public LeaveRequestResponse(string message) : base(message)
        {
        }
    }
}
