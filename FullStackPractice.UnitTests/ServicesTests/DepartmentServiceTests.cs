using AutoFixture;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using FullStackPractice.Business;
using FullStackPractice.Business.Interfaces;
using FullStackPractice.Common.AutoMapper;
using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Persistence;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Services.Constants;
using FullStackPractice.Validations;
using FullStackPractice.Validations.Interfaces;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System;
using FullStackPractice.Services;
using System.Linq.Expressions;

namespace FullStackPractice.UnitTests.ServicesTests
{
    public class DepartmentServiceTests
    {
        private IMapper _mapper;
        private Fixture _fixture;
        private Mock<IDepartmentRepository> _mockDepartmentRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IValidator<Department>> _mockDepartmentValidator;

        public DepartmentServiceTests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mapperConfig.CreateMapper();
            _fixture = new Fixture();

            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockDepartmentValidator = new Mock<IValidator<Department>>();
        }

        [Fact]
        public async Task GetAllDepartments_ReturnsCorrectResult()
        {
            // Arrange
            var mockDepartmentsEntity = _fixture.CreateMany<Department>(2).ToList();
            var expected = _mapper.Map<List<DepartmentDto>>(mockDepartmentsEntity);

            _mockDepartmentRepository.Setup(mdr => mdr.GetAllAsync()).ReturnsAsync(mockDepartmentsEntity).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);
            var serviceResponse = await sut.GetAllDepartmentsAsync();

            // Assert
            _mockDepartmentRepository.Verify();
            Assert.NotNull(serviceResponse);
            Assert.IsType<List<DepartmentDto>>(serviceResponse);
            Assert.Equal(expected.Count, serviceResponse.Count);
        }

        [Fact]
        public async Task GetDepartmentById_ReturnsCorrectResult()
        {
            // Arrange
            var mockDepartmentEntity = _fixture.Create<Department>();
            var expected = _mapper.Map<DepartmentDto>(mockDepartmentEntity);

            _mockDepartmentRepository.Setup(mdr => mdr.GetByIdAsync(mockDepartmentEntity.DepartmentId)).ReturnsAsync(mockDepartmentEntity).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);
            var actual = await sut.GetDepartmentByIdAsync(mockDepartmentEntity.DepartmentId);

            // Assert
            _mockDepartmentRepository.Verify();
            Assert.NotNull(actual);
            Assert.IsType<DepartmentDto>(actual);
            Assert.Equal(expected.DepartmentName, actual.DepartmentName);
            Assert.Equal(expected.DepartmentId, actual.DepartmentId);
        }

        [Fact]
        public async Task CreateDepartment_Success()
        {
            // Arrange
            var mockDepartment = _fixture.Create<Department>();
            var mockDepartmentDto = _fixture.Create<DepartmentDto>();
            mockDepartmentDto.DepartmentId = 0;

            _mockDepartmentRepository.Setup(mdr => mdr.FindAsync(It.IsAny<Expression<Func<Department, bool>>>())).ReturnsAsync(new List<Department>()).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.AddAsync(It.IsAny<Department>())).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            var _mockValidationResult = new Mock<FluentValidation.Results.ValidationResult>();
            _mockValidationResult.Setup(x => x.IsValid).Returns(true);

            _mockDepartmentValidator.Setup(x => x.ValidateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockValidationResult.Object);

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);
            var actual = await sut.CreateDepartmentAsync(mockDepartmentDto);

            // Assert
            _mockDepartmentRepository.Verify(x => x.AddAsync(It.IsAny<Department>()), Times.Once);
            Assert.NotNull(actual);
            Assert.IsType<DepartmentDto>(actual);
        }

        [Fact]
        public async Task CreateDepartment_Error_DepartmentName_IsEmpty()
        {
            // Arrange
            var mockDepartmentDto = _fixture.Create<DepartmentDto>();
            var mockDepartment = _mapper.Map<Department>(mockDepartmentDto);

            _mockDepartmentRepository.Setup(mdr => mdr.AddAsync(It.IsAny<Department>())).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            var _mockValidationResult = new Mock<FluentValidation.Results.ValidationResult>();

            _mockDepartmentValidator.Setup(x => x.ValidateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
                                         .ReturnsAsync(new FluentValidation.Results.ValidationResult(new List<ValidationFailure>()
                                         {
                                            new ValidationFailure("DepartmentName",ValidationMessages.DepartmentNameMustNotBeEmpty){ErrorCode = "400"}
                                         }));

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);

            // Assert
            Func<Task> action = async () => await sut.CreateDepartmentAsync(mockDepartmentDto);

            await action.Should().ThrowAsync<ServiceException>(ValidationMessages.DepartmentNameMustNotBeEmpty);
        }

        [Fact]
        public async Task CreateDepartment_Error_DepartmentName_IsNotUnique()
        {
            // Arrange
            var mockDepartmentDto = _fixture.Create<DepartmentDto>();
            var mockDepartment = _mapper.Map<Department>(mockDepartmentDto);

            var mockExistingDepartmentName = _fixture.CreateMany<Department>(1).ToList();
            
            _mockDepartmentRepository.Setup(mdr => mdr.FindAsync(It.IsAny<Expression<Func<Department, bool>>>())).ReturnsAsync(mockExistingDepartmentName).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.AddAsync(It.IsAny<Department>())).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            var _mockValidationResult = new Mock<FluentValidation.Results.ValidationResult>();
            _mockValidationResult.Setup(x => x.IsValid).Returns(true);

            _mockDepartmentValidator.Setup(x => x.ValidateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockValidationResult.Object);

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);

            // Assert
            Func<Task> action = async () => await sut.CreateDepartmentAsync(mockDepartmentDto);

            await action.Should().ThrowAsync<ServiceException>(ValidationMessages.DepartmentNameMustBeUnique);
        }

        [Fact]
        public async Task UpdateDepartment_Success()
        {
            // Arrange
            var mockDepartment = _fixture.Create<Department>();
            var mockDepartmentDto = _fixture.Create<DepartmentDto>();
            mockDepartmentDto.DepartmentId = 0;

            _mockDepartmentRepository.Setup(mdr => mdr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mockDepartment).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.FindAsync(It.IsAny<Expression<Func<Department, bool>>>())).ReturnsAsync(new List<Department>()).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.UpdateAsync(It.IsAny<Department>())).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            var _mockValidationResult = new Mock<FluentValidation.Results.ValidationResult>();
            _mockValidationResult.Setup(x => x.IsValid).Returns(true);

            _mockDepartmentValidator.Setup(x => x.ValidateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockValidationResult.Object);

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);
            var actual = await sut.UpdateDepartmentAsync(mockDepartmentDto);

            // Assert
            _mockDepartmentRepository.Verify(x => x.UpdateAsync(It.IsAny<Department>()), Times.Once);
            Assert.NotNull(actual);
            Assert.IsType<DepartmentDto>(actual);
        }

        [Fact]
        public async Task UpdateDepartment_Error_DepartmentNotFound()
        {
            // Arrange
            Department mockDepartment = null;
            var mockDepartmentDto = _fixture.Create<DepartmentDto>();
   
            _mockDepartmentRepository.Setup(mdr => mdr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mockDepartment).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.FindAsync(It.IsAny<Expression<Func<Department, bool>>>())).ReturnsAsync(new List<Department>()).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.UpdateAsync(It.IsAny<Department>())).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            var _mockValidationResult = new Mock<FluentValidation.Results.ValidationResult>();
            _mockValidationResult.Setup(x => x.IsValid).Returns(true);

            _mockDepartmentValidator.Setup(x => x.ValidateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockValidationResult.Object);

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);
            Func<Task> action = async () => await sut.UpdateDepartmentAsync(mockDepartmentDto);

            // Assert
            await action.Should().ThrowAsync<ServiceException>(ValidationMessages.DepartmentNotFound);
        }

        [Fact]
        public async Task UpdateDepartment_Error_DepartmentName_IsEmpty()
        {
            // Arrange
            var mockDepartmentDto = _fixture.Create<DepartmentDto>();
            var mockDepartment = _mapper.Map<Department>(mockDepartmentDto);

            _mockDepartmentRepository.Setup(mdr => mdr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mockDepartment).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.FindAsync(It.IsAny<Expression<Func<Department, bool>>>())).ReturnsAsync(new List<Department>()).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.UpdateAsync(It.IsAny<Department>())).Verifiable();

            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            var _mockValidationResult = new Mock<FluentValidation.Results.ValidationResult>();

            _mockDepartmentValidator.Setup(x => x.ValidateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
                                         .ReturnsAsync(new FluentValidation.Results.ValidationResult(new List<ValidationFailure>()
                                         {
                                            new ValidationFailure("DepartmentName",ValidationMessages.DepartmentNameMustNotBeEmpty){ErrorCode = "400"}
                                         }));

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);
            Func<Task> action = async () => await sut.UpdateDepartmentAsync(mockDepartmentDto);

            // Assert
            await action.Should().ThrowAsync<ServiceException>(ValidationMessages.DepartmentNameMustNotBeEmpty);
        }

        [Fact]
        public async Task UpdateDepartment_Error_DepartmentName_IsNotUnique()
        {
            // Arrange
            var mockDepartmentDto = _fixture.Create<DepartmentDto>();
            var mockDepartment = _mapper.Map<Department>(mockDepartmentDto);

            var mockExistingDepartmentName = _fixture.CreateMany<Department>(1).ToList();

            _mockDepartmentRepository.Setup(mdr => mdr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mockDepartment).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.FindAsync(It.IsAny<Expression<Func<Department, bool>>>())).ReturnsAsync(mockExistingDepartmentName).Verifiable();

            _mockDepartmentRepository.Setup(mdr => mdr.UpdateAsync(It.IsAny<Department>())).Verifiable();


            _mockUnitOfWork.Setup(m => m.DepartmentRepository).Returns(_mockDepartmentRepository.Object);

            var _mockValidationResult = new Mock<FluentValidation.Results.ValidationResult>();
            _mockValidationResult.Setup(x => x.IsValid).Returns(true);

            _mockDepartmentValidator.Setup(x => x.ValidateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>())).ReturnsAsync(_mockValidationResult.Object);

            // Act
            IDepartmentService sut = new DepartmentService(_mockUnitOfWork.Object, _mapper, _mockDepartmentValidator.Object);
            Func<Task> action = async () => await sut.UpdateDepartmentAsync(mockDepartmentDto);

            // Assert
            await action.Should().ThrowAsync<ServiceException>(ValidationMessages.DepartmentNameMustBeUnique);
        }

    }
}
