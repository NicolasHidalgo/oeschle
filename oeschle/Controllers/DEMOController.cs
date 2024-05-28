using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oeschle.Models;
using oeschle.Repository.Interface;

namespace oeschle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DEMOController : ControllerBase
    {
        private readonly IEmployeeService<Employee> _employeeService;
        public DEMOController(IEmployeeService<Employee> employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeService.Get());
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _employeeService.Get(id));
        }
        [HttpGet("GetByNumber")]
        public async Task<IActionResult> Get(string document_number)
        {
            if (document_number.Length > 8)
            {
                return StatusCode(404, "Error por cantidad de dígitos");
            }
            return Ok(await _employeeService.Get(document_number));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Employee oEmpleado)
        {
            return Ok(await _employeeService.Save(oEmpleado));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] Employee oEmpleado)
        {
            return Ok(await _employeeService.Update(oEmpleado));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await _employeeService.Delete(id));
        }
    }
}
