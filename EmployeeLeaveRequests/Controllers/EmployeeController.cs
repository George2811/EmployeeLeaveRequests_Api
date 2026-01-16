using EmployeeLeaveRequests.Resources;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveRequests.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        //GET ALL
        //[RBACAuthorize(ActionTag = "CAN-GET-AUTHPROVIDERS")]
        //[HttpGet("[action]"), Produces("application/json")]
        //[ProducesResponseType(typeof(IEnumerable<EmployeeResource>), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IEnumerable<EmployeeResource>> Get()
        //{
        //    var result = new List<EmployeeResource>();
        //    return Ok(result);

        //}

        [HttpPost("[action]"), Produces("application/json")]
        public async Task<IActionResult> CreateNewRequest()
        {
            return await Task.Run(() =>
            {
                return Ok();
            });
        }

        [HttpPut("[action]"), Produces("application/json")]
        public async Task<IActionResult> UpdateStatus()
        {
            return await Task.Run(() =>
            {
                return Ok();
            });
        }

        [HttpDelete("[action]"), Produces("application/json")]
        public async Task<IActionResult> Cancel()
        {
            return await Task.Run(() =>
            {
                return Ok();
            });
        }
    }
}
