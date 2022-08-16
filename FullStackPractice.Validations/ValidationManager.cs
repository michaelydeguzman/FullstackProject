using FullStackPractice.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Validations
{
    public class ValidationManager : IValidationManager
    {
        public DepartmentValidator Department { get; set; }
        public CreateEmployeeValidator CreateEmployee { get; set; }
        public UpdateEmployeeValidator UpdateEmployee { get; set; }
        public DeleteEmployeeValidator DeleteEmployee { get; set; }

        public ValidationManager(DepartmentValidator department, CreateEmployeeValidator createEmployee, UpdateEmployeeValidator updateEmployee, 
            DeleteEmployeeValidator deleteEmployee)
        {
            Department = department;
            CreateEmployee = createEmployee;
            UpdateEmployee = updateEmployee;
            DeleteEmployee = deleteEmployee;
        }
    }
}
