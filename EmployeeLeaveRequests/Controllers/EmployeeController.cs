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
        [HttpGet("[action]"), Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() =>
            {
                //var result = _authProviderInterface.GetAuthProviders();
                return Ok();
            });
        }

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
