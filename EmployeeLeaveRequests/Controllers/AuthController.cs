using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using EmployeeLeaveRequests.Services.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveRequests.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtTokenService _jwt;

        public AuthController(
            IEmployeeRepository repo,
            IJwtTokenService jwt)
        {
            _employeeRepository = repo;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var employee = await _employeeRepository.GetByEmailAsync(request.Email);

            if (employee == null)
                return Unauthorized();

            // ⚠️ Simplificado (en prod usa hashing)
            if (employee.Password != request.Password)
                return Unauthorized();

            var token = _jwt.GenerateToken(employee);

            return Ok(new { token });
        }
    }
}
