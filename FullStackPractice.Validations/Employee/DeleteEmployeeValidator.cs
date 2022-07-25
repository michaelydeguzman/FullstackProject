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
    public class DeleteEmployeeValidator : AbstractValidator<Employee>
    {
        private IUnitOfWork _unitOfWork;

        public DeleteEmployeeValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.EmployeeId).MustAsync(async (employeeId, cancellation) =>
            {
                var employees = await _unitOfWork.EmployeeRepository.FindAsync(x => x.EmployeeId == employeeId);
                return employees.Count() == 0;
            }).WithMessage(ValidationMessages.EmployeeNotFound);
        }
    }
}
