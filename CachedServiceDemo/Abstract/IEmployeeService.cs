using CachedServiceDemo.Dtos;
using CachedServiceDemo.Entites;

namespace CachedServiceDemo.Abstract
{
    public interface IEmployeeService
    {
        public List<Employee> Get();
        public bool Create(EmployeeDto employeeDto);
    }
}
