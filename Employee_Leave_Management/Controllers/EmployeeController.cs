using Employee_Leave_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Employee_Leave_Management.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // -------------------------------------------------------------
        // 1. RETRIEVAL PROGRAM: Fetch all employees from DB
        // -------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

        // GET: Create Employee Form
        public IActionResult Create()

        {

            return View();

        }

        // POST: Create Employee
        // -------------------------------------------------------------
        // 2. STORE PROGRAM: Save new employee details to DB
        // -------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);        // Add to EF Context
                await _context.SaveChangesAsync();       // Commit to SQL Database
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Edit Employee Form
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Edit Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = await _context.Employees.FindAsync(model.Id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = model.Name;
            employee.Department = model.Department;
            employee.Email = model.Email;
            employee.Password = model.Password;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/DeleteConfirmed/5 (Action that performs the delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);   // Mark record for deletion
                await _context.SaveChangesAsync();     // Execute DELETE SQL query
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Dashboard()
        {
            // Fetch all leave requests from database
            var requests = await _context.LeaveRequests.ToListAsync();
            return View(requests);
        }


    }
}
//Index(): Retrieves all employees from the database and passes them to the View.
//Create(Employee employee): Receives submitted form data stores it in the database
//via AppDbContext, and redirects back to the index list.