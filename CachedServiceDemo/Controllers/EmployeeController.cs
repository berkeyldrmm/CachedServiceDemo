using CachedServiceDemo.Abstract;
using CachedServiceDemo.Dtos;
using CachedServiceDemo.Entites;
using CachedServiceDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CachedServiceDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IEmployeeService _employeeService { get; set; }
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _employeeService.Get();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EmployeeDto employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee cannot be null");
            }
            var result = _employeeService.Create(employee);
            if (result)
                return Created();

            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating employee");
        }
    }
}
