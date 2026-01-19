using AutoMapper;
using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Resources;

namespace EmployeeLeaveRequests.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Employee, EmployeeResource>();
            CreateMap<LeaveRequest, LeaveRequestResource>();
            CreateMap<LeaveRequest, LeaveRequestDetailedResource>();
        }
    }
}
