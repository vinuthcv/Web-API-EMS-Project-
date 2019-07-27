using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Net;
using Moq;
using EmployeeCore;
using System.Web.Http;

namespace EmployeeWebApi.Controllers.Tests
{
    [TestClass()]
    public class EmployeesControllerTests
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

            var mockEmployeemanager = new Mock<IEmployeeManager>();
            mockEmployeemanager.Setup(x => x.AddEmployee(employee)).Returns(2);

            //act Calling the method with mocked data.
            var controller = new EmployeesController(mockEmployeemanager.Object);
            IHttpActionResult result = controller.Post(employee);
            var negResult = result as NegotiatedContentResult<string>;


            //assert test data and action.
            Assert.IsNotNull(negResult);
            Assert.IsNotNull(negResult.Content);
            Assert.AreEqual(negResult.StatusCode, HttpStatusCode.Created);

            //var responseContent = negResult.Content.ToString();
            //int.TryParse(responseContent.Substring(responseContent.LastIndexOf(" "),responseContent.Length - responseContent.LastIndexOf(" ")), out employeeId);
            //Assert.IsTrue(negResult.Content.ToString().Contains("Employee created successfully"));
        }

        [TestMethod()]
        public void AddEmployeeErrorTest()
        {
            //arranger the test data
            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = null
            };
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager
                .Setup(x => x.AddEmployee(employee))
                .Returns(2)
                .Callback(() =>
                {
                    throw new Exception("Username cannot be null");
                });

            // act calling method with test/mock data.
            var controller = new EmployeesController(mockEmployeeManager.Object);
            IHttpActionResult result = controller.Post(employee);
            var negResult = result as NegotiatedContentResult<string>;

            //assert: verifying the result
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExceptionResult));
        }

        [TestMethod()]
        public void GetEmployeesTest()
        {
            //arrange: the mock data creation.
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
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager
                .Setup(x => x.GetEmployees())
                .Returns(employees);

            //act: calling method with mocked object
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Get();
            var negResult = result as OkNegotiatedContentResult<IEnumerable<EmployeeEntities.Employee>>;

            //assert: verifying the result
            Assert.IsNotNull(negResult);
            Assert.IsTrue(negResult.Content.Any());
        }

        [TestMethod()]
        public void GetEmployeeByIdTest()
        {
            int employeeId = 2;
            //arrange: mock test data
            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "vinuth" + DateTime.Now.Millisecond + "",
                Id=2
            };
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager
                .Setup(x => x.GetEmployee(employeeId))
                .Returns(employee);
                
            // act: test mocked data.
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Get(employeeId);
            var negResult = result as OkNegotiatedContentResult<EmployeeEntities.Employee>;

            //assert: verify the results.
            Assert.IsNotNull(negResult);
            Assert.IsTrue(negResult.Content.Id == employeeId);
        }


        [TestMethod()]
        public void GetEmployeeByIdNotFoundTest()
        {
            int employeeId = 0;
            //arrange: mock test data
            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "vinuth" + DateTime.Now.Millisecond + "",
                Id = 2
            };
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager
                .Setup(x => x.GetEmployee(0))
                .Returns(employee);

            // act: test mocked data.
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Get(employeeId);
            var negResult = result as NotFoundResult;

            //assert: verify the results.
            Assert.IsNotNull(negResult);
        }

        [TestMethod()]
        public void DeleteEmployeeByIdNotFoundTest()
        {
            //arrange: mocking the data.

            var mockEmployeeManager = new Mock<IEmployeeManager>();

            //act: test mocked data.
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Delete(0);
            var negResult = result as StatusCodeResult;

            //assert: verify the results.
            Assert.IsNotNull(result);            
            Assert.AreEqual(negResult.StatusCode,HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public void DeleteEmployeeByIdTest()
        {
            //arrange: mock the data.
            var employeeId = 2;
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager
                .Setup(x => x.DeleteEmployee(employeeId))
                .Returns(true);
                
            //act: call the method with mocked data.
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Delete(employeeId);
            var negResult = result as StatusCodeResult;

            //assert: verify the results.
            Assert.IsNotNull(negResult);
            Assert.AreEqual(negResult.StatusCode, HttpStatusCode.NoContent);

        }

        [TestMethod()]
        public void CheckLoginSuccessMethod()
        {
            //arrange
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager
                .Setup(x => x.CheckLogin("vinuth", "test123"))
                .Returns(true);

            //act 
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.CheckLogin(new Models.Login { Password = "test123", UserName="vinuth"});
            var negResult = result as NegotiatedContentResult<string>;

            //assert
            Assert.IsNotNull(negResult);
            Assert.AreEqual(negResult.StatusCode,HttpStatusCode.OK);
        }


        [TestMethod()]
        public void CheckLoginFailMethod()
        {
            // arrange
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            mockEmployeeManager
                .Setup(x => x.CheckLogin("vinuth", "test123"))
                .Returns(false);

            //act 
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.CheckLogin(new Models.Login { Password = "test123", UserName = "vinuth" });
            var negResult = result as NegotiatedContentResult<string>;

            //assert
            Assert.IsNotNull(negResult);
            Assert.AreEqual(negResult.StatusCode, HttpStatusCode.Forbidden);
        }
    }
}