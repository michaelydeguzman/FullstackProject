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
        public const string DepartmentNotFound = "Department id does not exist.";

        public const string EmployeeNameMustNotBeEmpty = "Employee name must not be empty.";
        public const string EmployeePasswordMustNotBeEmpty = "Please specify a password.";
        public const string EmployeePasswordLengthInvalid = "Password must be at least 6 characters";
        public const string EmployeeNotFound = "Employee id does not exist.";
        public const string EmailAddressTaken = "Email address is already taken.";

    }
}
