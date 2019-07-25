using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Net;

namespace EmployeeWebApi.Controllers.Tests
{
    [TestClass()]
    public class EmployeesControllerTests
    {
        int employeeId = 17;
        [TestMethod()]
        public void AddEmployeeTest()
        {
            var controller = new EmployeesController(IEmployeeManger employeemanager);
            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = "vinuth" + DateTime.Now.Millisecond + ""
            };
            var result = controller.Post(employee);

            Assert.IsNotNull(result);

            var negResult = result as NegotiatedContentResult<string>;

            var responseContent = negResult.Content.ToString();
            int.TryParse(responseContent.Substring(responseContent.LastIndexOf(" "),responseContent.Length - responseContent.LastIndexOf(" ")), out employeeId);
            Assert.IsTrue(negResult.Content.ToString().Contains("Employee created successfully"));
        }

        [TestMethod()]
        public void AddEmployeeErrorTest()
        {
            var controller = new EmployeesController();
            var employee = new EmployeeEntities.Employee
            {
                DateOfBirth = "27-04-1995",
                Email = "vinuthcv@abc.com",
                FullName = "vinuth vish",
                Gender = "Male",
                Password = "test123",
                Username = null
            };
            var result = controller.Post(employee);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExceptionResult));
        }

        [TestMethod()]
        public void GetEmployeesTest()
        {
            var controller = new EmployeesController();
            var result = controller.Get();
            Assert.IsNotNull(result);

            var negResult = result as OkNegotiatedContentResult<IEnumerable<EmployeeEntities.Employee>>;

            Assert.IsTrue(negResult.Content.Any());
        }

        [TestMethod()]
        public void GetEmployeeByIdTest()
        {
            var controller = new EmployeesController();
            var result = controller.Get(employeeId);
            Assert.IsNotNull(result);

            var negResult = result as OkNegotiatedContentResult<EmployeeEntities.Employee>;

            Assert.IsTrue(negResult.Content.Id == employeeId);
        }


        [TestMethod()]
        public void GetEmployeeByIdNotFoundTest()
        {
            var controller = new EmployeesController();
            var result = controller.Get(0);
            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void DeleteEmployeeByIdNotFoundTest()
        {
            var controller = new EmployeesController();
            var result = controller.Delete(0);
            Assert.IsNotNull(result);
            var negResult = result as StatusCodeResult;
            Assert.AreEqual(negResult.StatusCode,HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public void DeleteEmployeeByIdTest()
        {
            var controller = new EmployeesController();
            var result = controller.Delete(employeeId);
            Assert.IsNotNull(result);
        }
    }
}