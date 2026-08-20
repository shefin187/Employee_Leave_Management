using Microsoft.AspNetCore.Mvc;
using Employee_Leave_Management.Models;

namespace Employee_Leave_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // 1. Check if email exists in the database
            var user = _context.Employees.FirstOrDefault(e => e.Email == email);

            if (user == null)
            {
                // Email doesn't exist
                ViewBag.ErrorMessage = "Email address not found.";
                return View();
            }

            // 2. Check if password matches
            if (user.Password != password)
            {
                // Email found, but password is wrong
                ViewBag.ErrorMessage = "Incorrect password.";
                return View();
            }

            // 3. Credentials are valid: Store user details in session
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);

            // 4. Redirect based on role
            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("EmployeeDashboard", "Account");
            }
        }

        public IActionResult EmployeeDashboard()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}