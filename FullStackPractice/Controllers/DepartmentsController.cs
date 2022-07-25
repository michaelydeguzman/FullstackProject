using FullStackPractice.Business.Interfaces;
using FullStackPractice.Contracts;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IServiceWrapper _serviceWrapper;


        public DepartmentsController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _serviceWrapper.DepartmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _serviceWrapper.DepartmentService.GetDepartmentByIdAsync(id);
            return Ok(department);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateDepartment(DepartmentDto departmentDto)
        {
            var result = await _serviceWrapper.DepartmentService.CreateDepartmentAsync(departmentDto);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {
            var result = await _serviceWrapper.DepartmentService.UpdateDepartmentAsync(departmentDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _serviceWrapper.DepartmentService.DeleteDepartmentAsync(id);
            return Ok();
        }
    }
}
