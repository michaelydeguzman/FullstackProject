﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Validations
{
    public interface IValidationManager
    {
        public CreateDepartmentValidator CreateDepartment { get; set; }

        public UpdateDepartmentValidator UpdateDepartment { get; set; }

        public DeleteDepartmentValidator DeleteDepartment { get; set; }
    }
}