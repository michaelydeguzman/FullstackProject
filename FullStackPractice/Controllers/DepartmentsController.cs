using FullStackPractice.Business.Interfaces;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Services.Dtos;
using FullStackPractice.Services.Interfaces;
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
        private readonly IServiceWrapper _serviceWrapper;

        public DepartmentsController(IConfiguration configuration, IServiceWrapper serviceWrapper)
        {
            _configuration = configuration;
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        public async Task<JsonResult> GetDepartments()
        {
            var departments = await _serviceWrapper.DepartmentService.GetAllDepartmentsAsync();
            return new JsonResult(departments);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResult> GetDepartmentById(int id)
        {
            var department = await _serviceWrapper.DepartmentService.GetDepartmentByIdAsync(id);
            return new JsonResult(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDto departmentDto)
        {
            try
            {
                await _serviceWrapper.DepartmentService.CreateDepartmentAsync(departmentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {
            try
            {
                await _serviceWrapper.DepartmentService.UpdateDepartmentAsync(departmentDto);
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
                await _serviceWrapper.DepartmentService.DeleteDepartmentAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
