using FluentValidation;
using FullStackPractice.Business;
using FullStackPractice.Business.Interfaces;
using FullStackPractice.Persistence;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FullStackPractice.UnitTests.ServicesTests
{
    public class DepartmentServiceTests
    {
        [Fact]
        public async Task GetAllDepartments_ReturnsCorrectResult()
        {
            // Arrange
            var expected = new List<Department>()
            {
                new Department { DepartmentId = 1, DepartmentName = "Test Department 1" },
                new Department { DepartmentId = 2, DepartmentName = "Test Department 2" }
            };

            var mockDepartmentValidator = new Mock<AbstractValidator<Department>>();

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();
            mockDepartmentRepository.Setup(mdr => mdr.GetAllAsync()).ReturnsAsync(expected).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(mockDepartmentRepository.Object);

            // Act
            IDepartmentService sut = new DepartmentService(mockUnitOfWork.Object, mockDepartmentValidator.Object);
            var actual = await sut.GetAllDepartmentsAsync();

            // Assert
            mockDepartmentRepository.Verify();
            Assert.IsType<List<Department>>(actual);
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetDepartmentById_ReturnsCorrectResult()
        {
            // Arrange
            var departmentId = 1;
            var expected = new Department { DepartmentId = departmentId, DepartmentName = "Test Department 1" };

            var mockDepartmentValidator = new Mock<AbstractValidator<Department>>();

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();
            mockDepartmentRepository.Setup(mdr => mdr.GetByIdAsync(departmentId)).ReturnsAsync(expected).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(mockDepartmentRepository.Object);

            // Act
            IDepartmentService sut = new DepartmentService(mockUnitOfWork.Object, mockDepartmentValidator.Object);
            var actual = await sut.GetDepartmentByIdAsync(departmentId);

            // Assert
            mockDepartmentRepository.Verify();
            Assert.IsType<Department>(actual);
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateDepartment_Success()
        {

        }
    }
}
