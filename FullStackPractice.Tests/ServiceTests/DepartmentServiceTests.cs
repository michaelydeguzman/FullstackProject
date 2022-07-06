using FullStackPractice.Business;
using FullStackPractice.Business.Interfaces;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FullStackPractice.Tests.ServiceTests
{
    [TestClass]
    public class DepartmentServiceTests
    {
        private Mock<IDepartmentService> _mockDepartmentService = new Mock<IDepartmentService>();
        private IDepartmentService _departmentService;

        public DepartmentServiceTests(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [TestMethod]
        public void GetAllDepartments()
        {
            _mockDepartmentService.Setup(svc => svc.GetAllDepartments()).ReturnsAsync(new List<Department> { new Department() });

            var departments = _departmentService.GetAllDepartments().Result;

            //var departments = Assert.IsType<List<Department>>(viewResult.Model);
            Assert.AreEqual(1, departments.Count);
        }

        //[Fact]
        //public async Task CreateDepartment_ResultIsSaved()
        //{
        //    var newDepartment = new Department()
        //    {
        //        DepartmentId = 0,
        //        DepartmentName = "Test Department"
        //    };


        //    mock.Setup(repo => 

        //    Assert.AreEqual(1, 1,);
        //}
    }
}
