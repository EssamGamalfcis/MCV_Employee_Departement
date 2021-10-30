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
    public class DepartmentController : ControllerBase
    {
        private readonly IDataRepository<Department> _dataRepository;
        public DepartmentController(IDataRepository<Department> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        [Route("Department/Get")]
        public async Task<IActionResult> Get([FromQuery]GetParam param)
        {
            GetDepartmentResponse departments = await _dataRepository.GetAllDepToUse(param);
            return Ok(departments);
        }

        [HttpGet]
        [Route("Department/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Department> departments = (List<Department>)await  _dataRepository.GetAll();
            return Ok(departments);
        }

        // GET: api/Employee/5
        [HttpGet]
        [Route("Department/GetById")]
        public IActionResult GetById(long id)
        {
            Department department =  _dataRepository.GetById(id);
            if (department == null)
            {
                return NotFound("The Department record couldn't be found.");
            }
            return Ok(department);
        }
        // POST: api/Employee
        [HttpPost]
        [Route("Department/Post")]
        public IActionResult Post([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Department is null.");
            }
            _dataRepository.Add(department);
            return Ok();
        }
        // PUT: api/Employee/5
        [HttpPut]
        [Route("Department/Put")]
        public IActionResult Put([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Department is null.");
            }
            Department employeeToUpdate =  _dataRepository.GetById(department.Id);
            if (employeeToUpdate == null)
            {
                return NotFound("The Department record couldn't be found.");
            }
            _dataRepository.Update(employeeToUpdate, department);
            return Ok();
        }
        // DELETE: api/Employee/5
        [HttpPost]
        [Route("Department/Delete")]
        public IActionResult Delete([FromBody] long id)
        {
            Department employee = _dataRepository.GetById(id);
            if (employee == null)
            {
                return NotFound("The Department record couldn't be found.");
            }
            bool apiRes = _dataRepository.Delete(id);
            if (apiRes)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
