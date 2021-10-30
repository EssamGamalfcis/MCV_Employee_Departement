using MCV_Employee_Department.API.Model;
using MCV_Employee_Department.API.ViewModel;
using MCV_Employee_Departement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MCV_Employee_Departement.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<ActionResult> Employee_Index(GetParam obj)
        {
            GetEmployeeResponse employees = new GetEmployeeResponse();
            using (var httpClient = new HttpClient())
            {
                string url = "https://localhost:44351/Employee/Get?PageIndex=" + obj.PageIndex + "&Name=" + obj.Name + "&DepartmentId=" + obj.DepartmentId;
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<GetEmployeeResponse>(apiResponse);
                }
            }
            using (var httpClient = new HttpClient())
            {
                string url = "https://localhost:44351/Department/GetAll";
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Departments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }
            return View(employees);
        }
        [HttpGet]
        public async Task<ActionResult> AddOrEditEmployee(long? id)
        {
            var model = new Employee();
            if (id != 0)
            {
                using (var httpClient = new HttpClient())
                {
                    string url = "https://localhost:44351/Employee/GetById?id=" + id;
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<Employee>(apiResponse);
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                string url = "https://localhost:44351/Department/GetAll";
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Departments = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                }
            }
            return PartialView("_AddOrEditEmployee", model);
        }



        public async Task<string> AddOrUpdateEmployee(Employee emp)
        {
            using (var client = new HttpClient())
            {
                var objToSer = JsonConvert.SerializeObject(emp);
                var content = new StringContent(objToSer, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44351/");
                var response = emp.Id == 0 ? await client.PostAsync("Employee/Post", content) : await client.PutAsync("Employee/Put", content);
                if (response.IsSuccessStatusCode)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
        }

        public async Task<string> DeleteEmployee(long id)
        {
            using (var client = new HttpClient())
            {
                var objToSer = JsonConvert.SerializeObject(id);
                var content = new StringContent(objToSer, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44351/");
                var response = await client.PostAsync("Employee/Delete", content);
                if (response.IsSuccessStatusCode)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
        }
    }
}
