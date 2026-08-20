using Employee_Leave_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Leave_Management.Controllers
{
    public class LeaveController : Controller
    {
        private readonly AppDbContext _context;

        public LeaveController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Leave / Module Index
        public async Task<IActionResult> Index()
        {
            // Populate employee list for the apply modal
            var employees = await _context.Employees.ToListAsync();
            ViewBag.EmployeeList = employees.Select(e => new SelectListItem
            {
                Value = e.Name,
                Text = e.Name
            }).ToList();

            var requests = await _context.LeaveRequests.ToListAsync();
            return View(requests);
        }

        // GET: Leave/Reports
        public async Task<IActionResult> Reports()
        {
            var requests = await _context.LeaveRequests.ToListAsync() ?? new List<LeaveRequest>();

            ViewBag.TotalRequests = requests.Count;
            ViewBag.ApprovedCount = requests.Count(r => r.LeaveStatus == "Approved");
            ViewBag.PendingCount = requests.Count(r => r.LeaveStatus == "Pending");
            ViewBag.RejectedCount = requests.Count(r => r.LeaveStatus == "Rejected");

            return View(requests);
        }

        // POST: Leave/Create (Submitted from the Apply Modal)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                leaveRequest.LeaveStatus = "Pending"; // Default status
                _context.LeaveRequests.Add(leaveRequest);
                await _context.SaveChangesAsync();

                // Redirect back to referring page or Dashboard
                return RedirectToAction("Index", "Home");
            }

            // If model validation fails, re-populate dropdown and reload current view
            var employees = await _context.Employees.ToListAsync();
            ViewBag.EmployeeList = employees.Select(e => new SelectListItem
            {
                Value = e.Name,
                Text = e.Name
            }).ToList();

            var requests = await _context.LeaveRequests.ToListAsync();
            return View("Index", requests);
        }

        // POST: Leave/UpdateStatus (Approve / Reject action)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var request = await _context.LeaveRequests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            request.LeaveStatus = status;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}