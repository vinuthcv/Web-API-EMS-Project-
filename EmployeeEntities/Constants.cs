using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEntities
{
#warning TODO: Update these commands as Store procedure.
    public class Constants
    {
        public const string InsertCommand = "INSERT INTO Employee(Username,Password,FullName,Email,DateOfBirth,Gender) OUTPUT INSERTED.ID VALUES(@Username,@Password,@FullName,@Email,@DateOfBirth,@Gender)";

        public const string DeleteCommand = "DELETE FROM Employee where Id = @Id";

        public const string GetEmployeesByIdCommand = "SELECT * from Employee where Id=@Id";

        public const string GetEmployeesCommand = "SELECT * from Employee";

        public const string ConnectionName = "EmployeeData";
    }
}
