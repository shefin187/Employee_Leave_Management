using Microsoft.EntityFrameworkCore;
using Employee_Leave_Management.Models;
namespace Employee_Leave_Management.Models;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
 
    { }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
}