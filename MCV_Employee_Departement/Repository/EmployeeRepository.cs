using MCV_Employee_Department.API.MCV_Context;
using MCV_Employee_Department.API.Model;
using MCV_Employee_Department.API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCV_Employee_Department.API.Repository
{
    public class EmployeeRepository : IDataRepository<Employee>
    {
        readonly MCVContext _employeeContext;
        private int totalCount = 0;
        public EmployeeRepository(MCVContext context)
        {
            _employeeContext = context;
        }
        public async Task<IEnumerable<Employee>> GetAll(GetParam param)
        {
            List<Employee> employees = new List<Employee>();
            totalCount = _employeeContext.Employee.Count(x => (param.Name == null || x.Name.Contains(param.Name)) && (param.DepartmentId == null || x.DepartmentID == param.DepartmentId) && x.IsDeleted != true);
            employees = await _employeeContext.Employee.Where(x => (param.Name == null || x.Name.Contains(param.Name)) && (param.DepartmentId == null || x.DepartmentID == param.DepartmentId) && x.IsDeleted != true).Skip(param.PageIndex * 10).Take(10).OrderByDescending(x=>x.CreationDate).ToListAsync();
            foreach (var employee in employees)
            {
                employee.DepartmentName = _employeeContext.Department.FirstOrDefault(x => x.Id == employee.DepartmentID).Name;
            }
            return employees;
        }
        public async Task<GetEmployeeResponse> GetAllEmpToUse(GetParam param)
        {
            GetEmployeeResponse retObj = new GetEmployeeResponse();
            retObj.Employees = (List<Employee>)await GetAll(param);
            retObj.TotalCount = totalCount;
            return retObj;
        }
        public  void Add(Employee entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.IsDeleted = false;
            entity.LastUpdateDate = DateTime.Now;
             _employeeContext.Employee.Add(entity);
             _employeeContext.SaveChanges();
        }
        public  void Update(Employee employee, Employee entity)
        {
            employee.Name = entity.Name;
            employee.Title = entity.Title;
            employee.BirthDate = entity.BirthDate;
            employee.HiringDate = entity.HiringDate;
            employee.CreationDate = DateTime.Now;
            employee.IsDeleted = false;
            employee.DepartmentID = entity.DepartmentID;
             _employeeContext.SaveChanges();
        }
        public bool Delete(long id)
        {
            var employee =  _employeeContext.Employee.FirstOrDefault(x => x.Id == id);
            employee.IsDeleted = true;
            _employeeContext.SaveChanges();
            return true;
        }
        public Employee GetById(long id)
        {
            return  _employeeContext.Employee.FirstOrDefault(x => x.Id == id && x.IsDeleted != true);
        }

        public Task<GetDepartmentResponse> GetAllDepToUse(GetParam param)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
