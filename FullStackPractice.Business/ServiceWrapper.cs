using FullStackPractice.Business.Interfaces;
using FullStackPractice.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Services
{
    public class ServiceWrapper : IServiceWrapper
    {
        public IDepartmentService DepartmentService { get; set; }
        public IEmployeeService EmployeeService { get; set; }

        public ServiceWrapper(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            DepartmentService = departmentService;
            EmployeeService = employeeService;
        }
    }
}
