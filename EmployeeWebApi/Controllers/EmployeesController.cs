using EmployeeCore;
using EmployeeEntities;
using EmployeeWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private IEmployeeManager _employeeManager;
        public EmployeesController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        // GET api/values
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var employees = _employeeManager.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var employee = _employeeManager.GetEmployee(id);
                if (employee != null && employee.Id == id)
                {
                    return Ok(employee);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post([FromBody]Employee emp)
        {
            try
            {
                var result = _employeeManager.AddEmployee(emp);
                if (result > 0)
                {
                    return Content(HttpStatusCode.Created, "Employee created successfully with ID " + result + "");
                }
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _employeeManager.DeleteEmployee(id);
                if (result)
                    return StatusCode(HttpStatusCode.NoContent);
                return StatusCode(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult CheckLogin([FromBody]Login login)
        {
            try
            {
                var result = _employeeManager.CheckLogin(login.UserName, login.Password);
                if(result)
                {
                    return Content(HttpStatusCode.OK, "Employee has authenticated successfully");
                }

                return Content(HttpStatusCode.Forbidden, "Invalid Username and Password");
            }
            catch (Exception ex)
            {
               return InternalServerError(ex);
            }
        }


    }
}
