using AutoMapper;
using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Resources;

namespace EmployeeLeaveRequests.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveEmployeeResource, Employee>();
            CreateMap<SaveLeaveRequestResource, LeaveRequest>();
            CreateMap<EmployeeResource, Employee>();
            CreateMap<LeaveRequestResource, LeaveRequest>();
        }
    }
}
