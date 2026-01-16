using AutoMapper;
using EmployeeLeaveRequests.Domain.Models;
using EmployeeLeaveRequests.Domain.Services;
using EmployeeLeaveRequests.Resources;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveRequests.Controllers
{
    [ApiController]
    [Route("api/leaverequests")]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, IMapper mapper)
        {
            _leaveRequestService = leaveRequestService;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}"), Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<LeaveRequestResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<LeaveRequestResource>> Get(Guid employeeId)
        {
            var leaveRequests = await _leaveRequestService.ListAsync(employeeId);

            var resources = _mapper.Map<IEnumerable<LeaveRequest>, IEnumerable<LeaveRequestResource>>(leaveRequests);

            return resources;
        }

        [HttpPost(), Produces("application/json")]
        [ProducesResponseType(typeof(LeaveRequestResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> CreateNewRequest([FromBody] SaveLeaveRequestResource resource)
        {
            var _leaveRequest = _mapper.Map<SaveLeaveRequestResource, LeaveRequest>(resource);

            var newLeaveRequest = await _leaveRequestService.SaveAsync(_leaveRequest);

            if (!newLeaveRequest.Success) return BadRequest(newLeaveRequest.Message);

            var _newResource = _mapper.Map<LeaveRequest, LeaveRequestResource>(newLeaveRequest.Resource);

            return Ok(_newResource);
        }

        [HttpPut("{leaveRequestId}"), Produces("application/json")]
        public async Task<IActionResult> UpdateStatus([FromBody] EmployeeResource resource, Guid leaveRequestId)
        {
            var _employee = _mapper.Map<EmployeeResource, Employee>(resource);

            var result = await _leaveRequestService.ApproveAsync(leaveRequestId, _employee);

            if(!result.Success) return BadRequest(result.Message);

            var _updatedResource = _mapper.Map<LeaveRequest, LeaveRequestResource>(result.Resource);

            return Ok(_updatedResource);
        }

        [HttpDelete("{leaveRequestId}/{employeeId}"), Produces("application/json")]
        public async Task<IActionResult> Cancel(Guid leaveRequestId, Guid employeeId)
        {
            var result = await _leaveRequestService.CancelAsync(leaveRequestId, employeeId);
            
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
