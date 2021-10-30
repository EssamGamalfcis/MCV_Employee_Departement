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
    public class DepartmentController : Controller
    {
        public async Task<ActionResult> Department_Index(GetParam obj)
        {
            GetDepartmentResponse departments = new GetDepartmentResponse();
            using (var httpClient = new HttpClient())
            {
                string url = "https://localhost:44351/Department/Get?PageIndex=" + obj.PageIndex + "&Name=" + obj.Name;
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    departments = JsonConvert.DeserializeObject<GetDepartmentResponse>(apiResponse);
                }
            }
            return View(departments);
        }
        [HttpGet]
        public async Task<ActionResult> AddOrEditDepartment(long? id)
        {
            var model = new Department();
            if (id != 0)
            {
                using (var httpClient = new HttpClient())
                {
                    string url = "https://localhost:44351/Department/GetById?id=" + id;
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<Department>(apiResponse);
                    }
                }
            }
            return PartialView("_AddOrEditDepartment", model);
        }



        public async Task<string> AddOrUpdateDepartment(Department dep)
        {
            using (var client = new HttpClient())
            {
                var objToSer = JsonConvert.SerializeObject(dep);
                var content = new StringContent(objToSer, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44351/");
                var response = dep.Id == 0 ? await client.PostAsync("Department/Post", content) : await client.PutAsync("Department/Put", content);
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

        public async Task<string> DeleteDepartment(long id)
        {
            using (var client = new HttpClient())
            {
                var objToSer = JsonConvert.SerializeObject(id);
                var content = new StringContent(objToSer, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("https://localhost:44351/");
                var response = await client.PostAsync("Department/Delete", content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
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
