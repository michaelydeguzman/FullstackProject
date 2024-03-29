﻿using FluentValidation;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Services.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Validations
{
    public class CreateEmployeeValidator : AbstractValidator<Employee>
    {
        private IUnitOfWork _unitOfWork;

        public CreateEmployeeValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.EmployeeName).NotEmpty().WithMessage(ValidationMessages.EmployeeNameMustNotBeEmpty);

            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
            {
                var departments = await _unitOfWork.EmployeeRepository.FindAsync(x => x.Email == email);
                return departments.Count() == 0;
            }).WithMessage(ValidationMessages.EmailAddressTaken);

            RuleFor(x => x.DepartmentId).MustAsync(async (departmentId, cancellation) =>
            {
                var departments = await _unitOfWork.DepartmentRepository.FindAsync(x => x.DepartmentId == departmentId);
                return departments.Count() == 1;
            }).WithMessage(ValidationMessages.DepartmentNotFound);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.EmployeePasswordMustNotBeEmpty)
                .Must(IsPasswordValid).WithMessage(ValidationMessages.EmployeePasswordLengthInvalid);
        }
        
        private bool IsPasswordValid(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }

            return true;
        }
    }
}
