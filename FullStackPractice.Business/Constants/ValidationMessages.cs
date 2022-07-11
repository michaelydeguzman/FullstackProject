using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Services.Constants
{
    public static class ValidationMessages
    { 
        public const string DepartmentNameMustNotBeEmpty = "Department name must not be empty.";
        public const string DepartmentNameMustBeUnique = "Department name already exists.";
        public const string DepartmentToBeDeletedMustNotHaveEmployees = "Department has current employees, cannot delete.";
    }
}
