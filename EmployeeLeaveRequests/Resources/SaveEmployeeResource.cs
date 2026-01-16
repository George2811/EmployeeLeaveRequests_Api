using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveRequests.Resources
{
    public class SaveEmployeeResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
