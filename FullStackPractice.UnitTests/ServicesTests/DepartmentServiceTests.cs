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

            var mockUpdateDepartmentValidator = new Mock<UpdateDepartmentValidator>(mockUnitOfWork.Object);
            var mockDeleteDepartmentValidator = new Mock<DeleteDepartmentValidator>(mockUnitOfWork.Object);

            // Act
            IDepartmentService sut = new DepartmentService(mockUnitOfWork.Object, mapper, mockUpdateDepartmentValidator.Object, mockDeleteDepartmentValidator.Object);
            var serviceResponse = await sut.GetAllDepartmentsAsync();

            // Assert
            mockDepartmentRepository.Verify();
            Assert.NotNull(serviceResponse);
            Assert.IsType<List<DepartmentDto>>(serviceResponse.Result);
            Assert.True(serviceResponse.IsSuccess);
            Assert.Equal(expected.Count, serviceResponse.Result.Count);
            //Assert.Equal(expected, serviceResponse.Result);
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

            var mockUpdateDepartmentValidator = new Mock<UpdateDepartmentValidator>(mockUnitOfWork.Object);
            var mockDeleteDepartmentValidator = new Mock<DeleteDepartmentValidator>(mockUnitOfWork.Object);

            // Act
            IDepartmentService sut = new DepartmentService(mockUnitOfWork.Object, mapper, mockUpdateDepartmentValidator.Object, mockDeleteDepartmentValidator.Object);
            var serviceResponse = await sut.GetDepartmentByIdAsync(departmentId);

            // Assert
            mockDepartmentRepository.Verify();
            Assert.NotNull(serviceResponse);
            Assert.IsType<DepartmentDto>(serviceResponse.Result);
            //Assert.Equal(expected, serviceResponse.Result);
        }

        [Fact]
        public async Task CreateDepartment_Success()
        {

        }
    }
}
