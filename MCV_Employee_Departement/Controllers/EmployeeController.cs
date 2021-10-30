using MCV_Employee_Department.API.MCV_Context;
using MCV_Employee_Department.API.Model;
using MCV_Employee_Department.API.Repository;
using MCV_Employee_Department.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCV_Employee_Department.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataRepository<Employee> _dataRepository;
        public EmployeeController(IDataRepository<Employee> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        [Route("Employee/Get")]
        public async Task<IActionResult> Get([FromQuery]GetParam param)
        {
            GetEmployeeResponse employees = await _dataRepository.GetAllEmpToUse(param);
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet]
        [Route("Employee/GetById")]
        public IActionResult GetById(long id)
        {
            Employee employee =  _dataRepository.GetById(id);
            if (employee == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            return Ok(employee);
        }
        // POST: api/Employee
        [HttpPost]
        [Route("Employee/Post")]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is null.");
            }
           _dataRepository.Add(employee);
            return Ok();
        }
        // PUT: api/Employee/5
        [HttpPut]
        [Route("Employee/Put")]
        public IActionResult Put([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is null.");
            }
            Employee employeeToUpdate =  _dataRepository.GetById(employee.Id);
            if (employeeToUpdate == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            _dataRepository.Update(employeeToUpdate, employee);
            return Ok();
        }
        // DELETE: api/Employee/5
        [HttpPost]
        [Route("Employee/Delete")]
        public IActionResult Delete([FromBody]long id)
        {
            Employee employee =  _dataRepository.GetById(id);
            if (employee == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            _dataRepository.Delete(id);
            return Ok();
        }

    }
}
