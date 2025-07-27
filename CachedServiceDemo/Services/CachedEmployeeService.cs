using CachedServiceDemo.Abstract;
using CachedServiceDemo.Dtos;
using CachedServiceDemo.Entites;
using Microsoft.Extensions.Caching.Memory;

namespace CachedServiceDemo.Services
{
    public class CachedEmployeeService : IEmployeeService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMemoryCache _memoryCache;
        public CachedEmployeeService(IEmployeeService employeeService, IMemoryCache memoryCache)
        {
            _employeeService = employeeService;
            _memoryCache = memoryCache;
        }

        public bool Create(EmployeeDto employeeDto)
        {
            var result = _employeeService.Create(employeeDto);
            if (result)
                _memoryCache.Remove("employees");

            return result;
        }

        public List<Employee> Get()
        {
            var employees = new List<Employee>();
            employees = _memoryCache.Get<List<Employee>>("employees");
            
            if(employees == null)
            {
                employees = _employeeService.Get();
                _memoryCache.Set("employees", employees);
            }

            return employees;
        }
    }
}
