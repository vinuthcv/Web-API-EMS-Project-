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
        int employeeId = 17;
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
            mockEmployeeManager.Setup(x => x.AddEmployee(employee)).Returns(2);

            var controller = new EmployeesController(mockEmployeeManager.Object);
            IHttpActionResult result = controller.Post(employee);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExceptionResult));
        }

        [TestMethod()]
        public void GetEmployeesTest()
        {
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Get();
            Assert.IsNotNull(result);

            var negResult = result as OkNegotiatedContentResult<IEnumerable<EmployeeEntities.Employee>>;

            Assert.IsTrue(negResult.Content.Any());
        }

        [TestMethod()]
        public void GetEmployeeByIdTest()
        {
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Get(employeeId);
            Assert.IsNotNull(result);

            var negResult = result as OkNegotiatedContentResult<EmployeeEntities.Employee>;

            Assert.IsTrue(negResult.Content.Id == employeeId);
        }


        [TestMethod()]
        public void GetEmployeeByIdNotFoundTest()
        {
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Get(0);
            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void DeleteEmployeeByIdNotFoundTest()
        {
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Delete(0);
            Assert.IsNotNull(result);
            var negResult = result as StatusCodeResult;
            Assert.AreEqual(negResult.StatusCode,HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public void DeleteEmployeeByIdTest()
        {
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.Delete(employeeId);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CheckLoginSuccessMethod()
        {
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.CheckLogin(new Models.Login { Password = "test123", UserName="vinuth15"});
            Assert.IsNotNull(result);

            var negResult = result as StatusCodeResult;
            Assert.AreEqual(negResult.StatusCode,HttpStatusCode.OK);
        }


        [TestMethod()]
        public void CheckLoginFailMethod()
        {
            var mockEmployeeManager = new Mock<IEmployeeManager>();
            var controller = new EmployeesController(mockEmployeeManager.Object);
            var result = controller.CheckLogin(new Models.Login { Password = "test1234", UserName = "vinuth15" });
            Assert.IsNotNull(result);

            var negResult = result as StatusCodeResult;
            Assert.AreEqual(negResult.StatusCode, HttpStatusCode.Forbidden);
        }
    }
}