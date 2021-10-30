using MCV_Employee_Department.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCV_Employee_Department.API.ViewModel
{
    public class GetParam
    {
        public int PageIndex { get; set; } = 0;
        public string Name { get; set; }
        public long? DepartmentId { get; set; }
    }
    public class GetEmployeeResponse
    { 
        public List<Employee> Employees { get; set; }
        public int TotalCount { get; set; }
    }
    public class DepartmentVM
    {
        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }
    }
    public class GetDepartmentResponse
    {
        public List<DepartmentVM> DepartmentsWithEmployees { get; set; }
        public int TotalCount { get; set; }
    }
}
