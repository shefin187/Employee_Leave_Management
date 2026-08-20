using System.ComponentModel.DataAnnotations;
namespace Employee_Leave_Management.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "Employee";
    }
}
// Employee Model (Models/Employee.cs)
//This defines the structure of the data to store in the database.