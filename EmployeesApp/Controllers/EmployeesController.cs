using EmployeesApp.Core.Models;
using EmployeesApp.Core.Services;
using EmployeesApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Controllers
{
    [Authorize]
	public class EmployeesController : Controller
    {
        private readonly IEmployeesService _service;

        public EmployeesController(IEmployeesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _service.GetAllAsync();
            var departments = await _service.GetDepartmentsAsync();
            var languages = await _service.GetProgrammingLanguagesAsync();

            var viewModel = new EmployeesViewModel
            {
                Departments = departments,
                Employees = employees,
                ProgrammingLanguages = languages
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
		{
			await _service.CreateAsync(employee);

			return RedirectToAction("Index");
        }

		[HttpPost]
		public async Task<IActionResult> Update(Employee employee)
		{
			await _service.UpdateAsync(employee);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int employeeId)
        {
            await _service.DeleteAsync(employeeId);

			return RedirectToAction("Index");
		}
    }
}
