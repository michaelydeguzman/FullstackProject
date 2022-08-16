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
    public class DepartmentValidator : AbstractValidator<Department>
    {
        private IUnitOfWork _unitOfWork;

        public DepartmentValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage(ValidationMessages.DepartmentNameMustNotBeEmpty);
        }
    }
}
