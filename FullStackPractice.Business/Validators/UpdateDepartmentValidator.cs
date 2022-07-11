using FluentValidation;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Services.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Services.Validations
{
    public class UpdateDepartmentValidator : AbstractValidator<Department>
    {
        private IUnitOfWork _unitOfWork;

        public UpdateDepartmentValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage(ValidationMessages.DepartmentNameMustNotBeEmpty);
            RuleFor(x => x.DepartmentName).MustAsync(async (departmentName, cancellation) =>
            {
                var departments = await _unitOfWork.DepartmentRepository.FindAsync(x=>x.DepartmentName == departmentName);
                return departments.Count() == 0;
            }).WithMessage(ValidationMessages.DepartmentNameMustBeUnique);
        }
    }
}
