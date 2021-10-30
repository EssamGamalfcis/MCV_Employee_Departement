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
    public class DepartmentRepository : IDataRepository<Department>
    {
        readonly MCVContext _departmentContext;
        public int totalCount { get; set; }
        public DepartmentRepository(MCVContext context)
        {
            _departmentContext = context;
        }
        public async Task<IEnumerable<Department>> GetAll(GetParam param)
        {
            totalCount = _departmentContext.Department.Count(x => (param.Name == null || x.Name.Contains(param.Name)) && x.IsDeleted != true);
            var departments = await _departmentContext.Department.Where(x => (param.Name == null || x.Name.Contains(param.Name)) && x.IsDeleted != true).Skip(param.PageIndex * 10).Take(10).OrderByDescending(x => x.CreationDate).ToListAsync();
           
            return departments;
        }
        public async Task<GetDepartmentResponse> GetAllDepToUse(GetParam param)
        {
            GetDepartmentResponse retObj = new GetDepartmentResponse();
            List<DepartmentVM> departmentsWithEmoloyees = new List<DepartmentVM>();
            List<Department> departments = (List<Department>)await GetAll(param);
            foreach (var department in departments)
            {
                DepartmentVM newObj = new DepartmentVM();
                newObj.Department = department;
                newObj.Employees = await _departmentContext.Employee.Where(x => x.DepartmentID == department.Id).ToListAsync();
                departmentsWithEmoloyees.Add(newObj);
            }
            retObj.DepartmentsWithEmployees = departmentsWithEmoloyees;
            retObj.TotalCount = totalCount;
            return retObj;
        }
        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _departmentContext.Department.Where(x =>x.IsDeleted != true).ToListAsync();
        }
        public async Task<Department> Get(long id)
        {
            return await _departmentContext.Department.FirstOrDefaultAsync(e => e.Id == id);
        }
        public  void Add(Department entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.IsDeleted = false;
            entity.LastUpdateDate = DateTime.Now;
             _departmentContext.Department.Add(entity);
             _departmentContext.SaveChanges();
        }
        public  void Update(Department department, Department entity)
        {
            department.Name = entity.Name;
            department.CreationDate = DateTime.Now;
            department.IsDeleted = false;
             _departmentContext.SaveChanges();
        }
        public bool Delete(long id)
        {
            if (_departmentContext.Employee.Any(x => x.DepartmentID == id && x.IsDeleted != true) == true) /*if the department have any emplyees*/
            {
                return false;
            }
            else
            {
                var employee = _departmentContext.Department.FirstOrDefault(x => x.Id == id);
                employee.IsDeleted = true;
                _departmentContext.SaveChanges();
                return true;
            }
        }
        public Department GetById(long id)
        {
            return  _departmentContext.Department.FirstOrDefault(x => x.Id == id && x.IsDeleted != true);
        }

        public Task<GetEmployeeResponse> GetAllEmpToUse(GetParam param)
        {
            throw new NotImplementedException();
        }
    }
}
