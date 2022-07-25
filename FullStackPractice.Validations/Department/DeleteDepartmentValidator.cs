using FluentValidation;
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
    public class DeleteDepartmentValidator : AbstractValidator<Department>
    {
        private IUnitOfWork _unitOfWork;

        public DeleteDepartmentValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Department test = new Department();

            RuleFor(x => x.DepartmentId).MustAsync(async (departmentId, cancellation) =>
            {
                var employees = await _unitOfWork.EmployeeRepository.FindAsync(x => x.DepartmentId == departmentId);
                return employees.Count() == 0;
            }).WithMessage(ValidationMessages.DepartmentToBeDeletedMustNotHaveEmployees);
        }
    }
}
