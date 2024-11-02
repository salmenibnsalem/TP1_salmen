using System.Collections.Generic;
using System.Linq;

namespace TP1_salmen.Models.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private List<Employee> employees = new()
        {
            new Employee { Id = 1, Name = "Sofien ben ali", Department = "comptabilité", Salary = 1000 },
            new Employee { Id = 2, Name = "Mourad triki", Department = "RH", Salary = 1500 },
            new Employee { Id = 3, Name = "Ali ben mohamed", Department = "informatique", Salary = 1700 },
            new Employee { Id = 4, Name = "Tarak aribi", Department = "informatique", Salary = 1100 }
        };

        public void Add(Employee employee)
        {
            employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
            employees.Add(employee);
        }

        public Employee FindByID(int id) => employees.FirstOrDefault(emp => emp.Id == id);
        public void Delete(int id) => employees.Remove(FindByID(id));
        public IList<Employee> GetAll() => employees;

        public void Update(int id, Employee newEmployee)
        {
            var employee = FindByID(id);
            if (employee != null)
            {
                employee.Name = newEmployee.Name;
                employee.Department = newEmployee.Department;
                employee.Salary = newEmployee.Salary;
            }
        }

        public List<Employee> Search(string term) =>
            string.IsNullOrEmpty(term) ? employees : employees.Where(e => e.Name.Contains(term)).ToList();

        public double SalaryAverage() => employees.Average(e => e.Salary);
        public double MaxSalary() => employees.Max(e => e.Salary);
        public int HrEmployeesCount() => employees.Count(e => e.Department == "RH");
    }
}
