using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeEntities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EmployeeData
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int AddEmployee(Employee emp)
        {
            try
            {
                var commandText = Constants.InsertCommand;
                using (SqlConnection conn = GetConnectionString())
                {
                    using (var cmd = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = emp.Username;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = emp.Password;
                        cmd.Parameters.Add("@FullName", SqlDbType.VarChar).Value = emp.FullName;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = emp.Email;
                        cmd.Parameters.Add("@DateOfBirth", SqlDbType.VarChar).Value = emp.DateOfBirth;
                        cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = emp.Gender;

                        var result = (int)cmd.ExecuteScalar();

                        return result;
                    }
                }
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
                var commandText = Constants.DeleteCommand;
                using (SqlConnection conn = GetConnectionString())
                {
                    using (var cmd = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;                        

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
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
                var commandText = Constants.GetEmployeesByIdCommand;
                using (SqlConnection conn = GetConnectionString())
                {
                    using (var cmd = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        var reader = cmd.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var employee = new Employee()
                                {
                                    DateOfBirth = (string)reader["DateOfBirth"],
                                    Email = (string)reader["Email"],
                                    FullName = (string)reader["FullName"],
                                    Gender = (string)reader["Gender"],
                                    Id = (int)reader["Id"],
                                    Password = (string)reader["Password"],
                                    Username = (string)reader["Username"]
                                };
                                return employee;
                            }
                        }

                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employee GetEmployee(string userName)
        {
            try
            {
                var commandText = Constants.GetEmployeesByUnameCommand;
                using (SqlConnection conn = GetConnectionString())
                {
                    using (var cmd = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = userName;

                        var reader = cmd.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var employee = new Employee()
                                {
                                    DateOfBirth = (string)reader["DateOfBirth"],
                                    Email = (string)reader["Email"],
                                    FullName = (string)reader["FullName"],
                                    Gender = (string)reader["Gender"],
                                    Id = (int)reader["Id"],
                                    Password = (string)reader["Password"],
                                    Username = (string)reader["Username"]
                                };
                                return employee;
                            }
                        }

                    }
                }
                return null;
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
                var employees = new List<Employee>();
                var commandText = Constants.GetEmployeesCommand;
                using (SqlConnection conn = GetConnectionString())
                {
                    using (var cmd = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var employee = new Employee()
                                {
                                    DateOfBirth = (string)reader["DateOfBirth"],
                                    Email = (string)reader["Email"],
                                    FullName = (string)reader["FullName"],
                                    Gender = (string)reader["Gender"],
                                    Id = (int)reader["Id"],
                                    Password = (string)reader["Password"],
                                    Username = (string)reader["Username"]
                                };

                                employees.Add(employee);
                            }
                        }
                    }
                }
                return employees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region private methods
        private static SqlConnection GetConnectionString()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConnectionName].ConnectionString);
        }
        #endregion
    }
}
