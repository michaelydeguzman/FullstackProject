using FullStackPractice.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Services.Interfaces
{
    public interface IServiceWrapper
    {
        public IDepartmentService DepartmentService { get; set; }
        public IEmployeeService EmployeeService { get; set; }
    }
}
