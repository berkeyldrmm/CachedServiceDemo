using CachedServiceDemo.Abstract;
using CachedServiceDemo.Context;
using CachedServiceDemo.Dtos;
using CachedServiceDemo.Entites;

namespace CachedServiceDemo.Services
{
    public class EmployeeService : IEmployeeService
    {
        public CachedServiceDemoDBContext _context { get; set; }

        public EmployeeService(CachedServiceDemoDBContext context)
        {
            _context = context;
        }

        public List<Employee> Get()
        {
            List<Employee> employees = _context.Employees.ToList();
            return employees;
        }

        public bool Create(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return false;
            }
            Employee employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Department = employeeDto.Department
            };
            _context.Employees.Add(employee);
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}
