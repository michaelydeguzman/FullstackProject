using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Validations.Interfaces
{
    public interface IValidationManager
    {
        public DepartmentValidator Department { get; set; }

        public CreateEmployeeValidator CreateEmployee { get; set; }

        public UpdateEmployeeValidator UpdateEmployee { get; set; }

        public DeleteEmployeeValidator DeleteEmployee { get; set; }


    }
}
