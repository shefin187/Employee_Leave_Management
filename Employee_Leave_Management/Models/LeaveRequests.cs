using System.ComponentModel.DataAnnotations;

namespace Employee_Leave_Management.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;

        public string LeaveStatus { get; set; } = "Pending";

    }
}