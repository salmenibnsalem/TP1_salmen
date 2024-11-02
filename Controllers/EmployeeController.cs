using TP1_salmen.Models;
using TP1_salmen.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TP1_salmen.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> employeeRepository;

        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }

        public IActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count;
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();
            return View(employees);
        }

        public IActionResult Details(int id) => View(employeeRepository.FindByID(id));

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Edit(int id) => View(employeeRepository.FindByID(id));

        [HttpPost]
        public IActionResult Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Update(id, employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Delete(int id) => View(employeeRepository.FindByID(id));

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            employeeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string term) => View("Index", employeeRepository.Search(term));
    }

}
