using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveRequests.Resources
{
    public class SaveLeaveRequestResource
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        
        public string Status { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
