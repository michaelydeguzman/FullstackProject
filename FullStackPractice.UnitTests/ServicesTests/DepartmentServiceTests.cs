using AutoMapper;
using FluentValidation;
using FullStackPractice.Business;
using FullStackPractice.Business.Interfaces;
using FullStackPractice.Common.AutoMapper;
using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Persistence;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Validations;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FullStackPractice.UnitTests.ServicesTests
{
    public class DepartmentServiceTests
    {
        private IMapper mapper;

        public DepartmentServiceTests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllDepartments_ReturnsCorrectResult()
        {
            // Arrange
            var mockDepartmentsEntity = new List<Department>()
            {
                new Department { DepartmentId = 1, DepartmentName = "Test Department 1" },
                new Department { DepartmentId = 2, DepartmentName = "Test Department 2" }
            };
            var expected = mapper.Map<List<DepartmentDto>>(mockDepartmentsEntity);

            var mockMapper = new Mock<IMapper>();

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();
            mockDepartmentRepository.Setup(mdr => mdr.GetAllAsync()).ReturnsAsync(mockDepartmentsEntity).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(mockDepartmentRepository.Object);

            var mockCreateDepartmentValidator = new Mock<CreateDepartmentValidator>(mockUnitOfWork.Object);
            var mockUpdateDepartmentValidator = new Mock<UpdateDepartmentValidator>(mockUnitOfWork.Object);
            var mockDeleteDepartmentValidator = new Mock<DeleteDepartmentValidator>(mockUnitOfWork.Object);

            var mockValidator = new Mock<IValidationManager>();
            mockValidator.Setup(x => x.CreateDepartment).Returns(mockCreateDepartmentValidator.Object);
            mockValidator.Setup(x => x.UpdateDepartment).Returns(mockUpdateDepartmentValidator.Object);
            mockValidator.Setup(x => x.DeleteDepartment).Returns(mockDeleteDepartmentValidator.Object);

            // Act
            IDepartmentService sut = new DepartmentService(mockUnitOfWork.Object, mapper, mockValidator.Object);
            var serviceResponse = await sut.GetAllDepartmentsAsync();

            // Assert
            mockDepartmentRepository.Verify();
            Assert.NotNull(serviceResponse);
            Assert.IsType<List<DepartmentDto>>(serviceResponse);
            Assert.Equal(expected.Count, serviceResponse.Count);
            //Assert.Equal(expected, serviceResponse);
        }

        [Fact]
        public async Task GetDepartmentById_ReturnsCorrectResult()
        {
            // Arrange
            var departmentId = 1;
            var mockDepartmentEntity = new Department { DepartmentId = departmentId, DepartmentName = "Test Department 1" };
            var expected = mapper.Map<DepartmentDto>(mockDepartmentEntity);

            var mockMapper = new Mock<IMapper>();

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();
            mockDepartmentRepository.Setup(mdr => mdr.GetByIdAsync(departmentId)).ReturnsAsync(mockDepartmentEntity).Verifiable();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(mockDepartmentRepository.Object);

            var mockCreateDepartmentValidator = new Mock<CreateDepartmentValidator>(mockUnitOfWork.Object);
            var mockUpdateDepartmentValidator = new Mock<UpdateDepartmentValidator>(mockUnitOfWork.Object);
            var mockDeleteDepartmentValidator = new Mock<DeleteDepartmentValidator>(mockUnitOfWork.Object);

            var mockValidator = new Mock<IValidationManager>();
            mockValidator.Setup(x => x.CreateDepartment).Returns(mockCreateDepartmentValidator.Object);
            mockValidator.Setup(x => x.UpdateDepartment).Returns(mockUpdateDepartmentValidator.Object);
            mockValidator.Setup(x => x.DeleteDepartment).Returns(mockDeleteDepartmentValidator.Object);

            // Act
            IDepartmentService sut = new DepartmentService(mockUnitOfWork.Object, mapper, mockValidator.Object);
            var actual = await sut.GetDepartmentByIdAsync(departmentId);

            // Assert
            mockDepartmentRepository.Verify();
            Assert.NotNull(actual);
            Assert.IsType<DepartmentDto>(actual);
            Assert.Equal(expected.DepartmentName, actual.DepartmentName);
        }

        [Fact]
        public async Task CreateDepartment_Success()
        {

        }
    }
}
