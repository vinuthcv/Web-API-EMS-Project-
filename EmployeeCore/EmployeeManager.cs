using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeEntities;
using EmployeeData;

namespace EmployeeCore
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeManager()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public int AddEmployee(Employee emp)
        {
            try
            {
                var result = _employeeRepository.AddEmployee(emp);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var employee = _employeeRepository.GetEmployee(id);
                if (employee != null && employee.Id == id)
                {
                    return _employeeRepository.DeleteEmployee(id);
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Employee GetEmployee(int id)
        {
            try
            {
                return _employeeRepository.GetEmployee(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                return _employeeRepository.GetEmployees();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
