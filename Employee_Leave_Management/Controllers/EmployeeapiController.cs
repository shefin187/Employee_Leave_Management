using Microsoft.AspNetCore.Mvc;
using Employee_Leave_Management.Models;

namespace Employee_Leave_Management.Controllers
{
    public class EmployeeapiController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeapiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Employee List
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _httpClient.GetFromJsonAsync<List<Employee>>(
                "https://localhost:7237/api/Employee");

            return View(employees);
        }

        // Add Employee Form
        [HttpGet]
        public IActionResult Index1()
        {
            return View(new Employee());
        }

        // Add Employee
        [HttpPost]
        public async Task<IActionResult> Index1(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7237/api/Employee",
                employee);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(employee);
        }
    }
}