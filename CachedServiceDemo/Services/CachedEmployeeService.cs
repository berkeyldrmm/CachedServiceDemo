using CachedServiceDemo.Abstract;
using CachedServiceDemo.Dtos;
using CachedServiceDemo.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CachedServiceDemo.Services
{
    public class CachedEmployeeService : IEmployeeService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDistributedCache _distributedCache;
        public CachedEmployeeService(IEmployeeService employeeService, IDistributedCache distributedCache)
        {
            _employeeService = employeeService;
            _distributedCache = distributedCache;
        }

        public bool Create(EmployeeDto employeeDto)
        {
            var result = _employeeService.Create(employeeDto);
            if (result)
                _distributedCache.Remove("employees");

            return result;
        }

        public List<Employee> Get()
        {
            var employees = new List<Employee>();
            var json = _distributedCache.GetString("employees");
            employees = json == null ? null : JsonSerializer.Deserialize<List<Employee>>(json);

            if (employees == null)
            {
                employees = _employeeService.Get();

                var jsonEmployees = JsonSerializer.Serialize(employees);
                _distributedCache.SetString("employees", jsonEmployees);
            }

            return employees;
        }
    }
}
