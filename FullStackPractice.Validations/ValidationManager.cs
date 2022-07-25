using FullStackPractice.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Validations
{
    public class ValidationManager : IValidationManager
    {
        public CreateDepartmentValidator CreateDepartment { get; set; }
        public UpdateDepartmentValidator UpdateDepartment { get; set; }
        public DeleteDepartmentValidator DeleteDepartment { get; set; }
        public CreateEmployeeValidator CreateEmployee { get; set; }
        public UpdateEmployeeValidator UpdateEmployee { get; set; }
        public DeleteEmployeeValidator DeleteEmployee { get; set; }

        public ValidationManager(CreateDepartmentValidator createDepartment, UpdateDepartmentValidator updateDepartment,
            DeleteDepartmentValidator deleteDepartment, CreateEmployeeValidator createEmployee, UpdateEmployeeValidator updateEmployee, 
            DeleteEmployeeValidator deleteEmployee)
        {
            CreateDepartment = createDepartment;
            UpdateDepartment = updateDepartment;
            DeleteDepartment = deleteDepartment;
            CreateEmployee = createEmployee;
            UpdateEmployee = updateEmployee;
            DeleteEmployee = deleteEmployee;
        }
    }
}
