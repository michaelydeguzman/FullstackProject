using FullStackPractice.Business.Interfaces;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IConfiguration configuration, IDepartmentService departmentService)
        {
            _configuration = configuration;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<JsonResult> GetDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return new JsonResult(departments);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            return new JsonResult(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            try
            {
                await _departmentService.CreateDepartmentAsync(department);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(Department department)
        {
            try
            {
                await _departmentService.UpdateDepartmentAsync(department);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
