using FullStackPractice.Business.Interfaces;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IWebHostEnvironment _env;

        public EmployeesController(IConfiguration configuration,
            IServiceWrapper serviceWrapper, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _serviceWrapper = serviceWrapper;
            _env = env;
        }

        [HttpGet]
        public async Task<JsonResult> GetEmployees()
        {
            var employees = await _serviceWrapper.EmployeeService.GetAllEmployeesAsync();
            return new JsonResult(employees);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResult> GetEmployeeById(int id)
        {
            var employee = await _serviceWrapper.EmployeeService.GetEmployeeByIdAsync(id);

            return new JsonResult(employee);
        }

        [HttpGet]
        [Route("Department/{id}")]
        public async Task<JsonResult> GetEmployeesByDepartmentId(int id)
        {
            var employee = await _serviceWrapper.EmployeeService.GetAllEmployeesByDepartmentId(id);
            return new JsonResult(employee);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            await _serviceWrapper.EmployeeService.CreateEmployeeAsync(employee);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            await _serviceWrapper.EmployeeService.UpdateEmployeeAsync(employee);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _serviceWrapper.EmployeeService.DeleteEmployeeAsync(id);
            return Ok();
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);

            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
