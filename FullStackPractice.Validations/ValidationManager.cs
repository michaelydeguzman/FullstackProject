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


        public ValidationManager(CreateDepartmentValidator createDepartment, UpdateDepartmentValidator updateDepartment, DeleteDepartmentValidator deleteDepartment)
        {
            CreateDepartment = createDepartment;
            UpdateDepartment = updateDepartment;
            DeleteDepartment = deleteDepartment;
        }
    }
}
