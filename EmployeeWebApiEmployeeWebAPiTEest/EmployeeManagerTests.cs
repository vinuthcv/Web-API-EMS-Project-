using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using EmployeeData;

namespace EmployeeCore.Tests
{
    [TestClass()]
    public class EmployeeManagerTests
    {
        [TestMethod()]
        public void AddEmployeeTest()
        {
            //arrange the test data with mocking.
            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "vinuth" + DateTime.Now.Millisecond + ""
            };
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository
                .Setup(x => x.AddEmployee(employee))
                .Returns(1);

            //arrange 
            var employeeManager = new EmployeeManager(mockEmployeeRepository.Object);
            var result = employeeManager.AddEmployee(employee);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 1);

        }

        [TestMethod()]
        public void DeleteEmployeeTest()
        {
            //arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository
                .Setup(x => x.DeleteEmployee(1))
                .Returns(true);

            //arrange 
            var employeeManager = new EmployeeManager(mockEmployeeRepository.Object);
            var result = employeeManager.DeleteEmployee(1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void DeleteEmployeeErrorTest()
        {
            //arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository
                .Setup(x => x.DeleteEmployee(0))
                .Returns(false);

            //arrange 
            var employeeManager = new EmployeeManager(mockEmployeeRepository.Object);
            var result = employeeManager.DeleteEmployee(0);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, false);
        }

        [TestMethod()]
        public void GetEmployeeTest()
        {
            //arrange
            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "vinuth" + DateTime.Now.Millisecond + "",
                Id = 1
            };
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository
                .Setup(x => x.GetEmployee(1))
                .Returns(employee);

            //arrange 
            var employeeManager = new EmployeeManager(mockEmployeeRepository.Object);
            var result = employeeManager.GetEmployee(1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, employee.Id);
        }       


        [TestMethod()]
        public void GetEmployeesTest()
        {
            var employees = new List<EmployeeEntities.Employee>
            {
                new EmployeeEntities.Employee{
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "VinuthTest1",
                Id = 1

                },
                new EmployeeEntities.Employee{
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "VinuthTest2",
                Id = 2
                }
            };
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository
                .Setup(x => x.GetEmployees())
                .Returns(employees);

            //act: calling method with mocked object
            var employeeManager = new EmployeeManager(mockEmployeeRepository.Object);
            var result = employeeManager.GetEmployees();

            //assert: verifying the result
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod()]
        public void CheckLoginTest()
        {

            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "vinuth" + DateTime.Now.Millisecond + "",
                Id = 1
            };

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository
                .Setup(x => x.GetEmployee(employee.Username))
                .Returns(employee);

            //act: calling method with mocked object
            var employeeManager = new EmployeeManager(mockEmployeeRepository.Object);
            var result = employeeManager.CheckLogin(employee.Username,employee.Password);

            //assert: verifying the result
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }
    }
}