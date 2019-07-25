using EmployeeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCore
{
    public interface IEmployeeManager
    {
        IEnumerable<Employee> GetEmployees();

        Employee GetEmployee(int id);

        int AddEmployee(Employee emp);

        bool DeleteEmployee(int id);

        bool CheckLogin(string userName, string key);
    }
}
