using System.Diagnostics;
using Employee_Leave_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Leave_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Central Dashboard
        public async Task<IActionResult> Index()
        {
            // 1. Fetch employees to populate the modal dropdown
            var employees = await _context.Employees.ToListAsync();
            ViewBag.EmployeeList = employees.Select(e => new SelectListItem
            {
                Value = e.Name, // Bind to employee name
                Text = e.Name
            }).ToList();

            // 2. Load summary stats for dashboard widgets
            ViewBag.TotalEmployees = employees.Count;
            ViewBag.PendingCount = await _context.LeaveRequests.CountAsync(r => r.LeaveStatus == "Pending");
            ViewBag.ApprovedCount = await _context.LeaveRequests.CountAsync(r => r.LeaveStatus == "Approved");
            ViewBag.RejectedCount = await _context.LeaveRequests.CountAsync(r => r.LeaveStatus == "Rejected");

            // 3. Get leave requests for main table
            var requests = await _context.LeaveRequests.ToListAsync();
            return View(requests);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}