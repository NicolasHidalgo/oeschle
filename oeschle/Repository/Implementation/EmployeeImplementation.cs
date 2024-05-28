using oeschle.Models;
using oeschle.Repository.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace oeschle.Repository.Implementation
{
    public class EmployeeImplementation : IEmployeeService<Employee>
    {
        private readonly string _cadenaSQL = "";
        public EmployeeImplementation(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cn");
        }

        public async Task<bool> Delete(long id)
        {
            var result = false;
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_EMPLOYEE", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 30).Value = "DELETE";
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = id;

                var row = cmd.ExecuteNonQuery();
                if (row > 0)
                    result = true;

            }
            return result;
        }

        public async Task<List<Employee>> Get()
        {
            List<Employee> lista = new List<Employee>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_EMPLOYEE", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 30).Value = "SELECT";

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Employee
                        {
                            id = Convert.ToInt64(dr["id"]),
                            name = dr["name"].ToString(),
                            document_number = dr["document_number"].ToString(),
                            salary = Convert.ToDouble(dr["salary"]),
                            age = Convert.ToInt32(dr["age"]),
                            profile = dr["profile"].ToString(),
                            admission_date = Convert.ToDateTime(dr["admission_date"]),
                        });
                    }
                }

            }
            return lista;
        }

        public async Task<Employee> Get(long id)
        {
            Employee obj = null;
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_EMPLOYEE", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 30).Value = "GETBY_ID";
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = id;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        obj = new Employee();
                        obj.id = Convert.ToInt64(dr["id"]);
                        obj.name = dr["name"].ToString();
                        obj.document_number = dr["document_number"].ToString();
                        obj.salary = Convert.ToDouble(dr["salary"]);
                        obj.age = Convert.ToInt32(dr["age"]);
                        obj.profile = dr["profile"].ToString();
                        obj.admission_date = Convert.ToDateTime(dr["admission_date"]);
                    }
                }

            }
            return obj;
        }

        public async Task<Employee> Get(string document_number)
        {
            Employee obj = null;
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_EMPLOYEE", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 30).Value = "GETBY_NUMBER";
                cmd.Parameters.Add("@DOCUMENT_NUMBER", System.Data.SqlDbType.BigInt).Value = document_number;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        obj = new Employee();
                        obj.id = Convert.ToInt64(dr["id"]);
                        obj.name = dr["name"].ToString();
                        obj.document_number = dr["document_number"].ToString();
                        obj.salary = Convert.ToDouble(dr["salary"]);
                        obj.age = Convert.ToInt32(dr["age"]);
                        obj.profile = dr["profile"].ToString();
                        obj.admission_date = Convert.ToDateTime(dr["admission_date"]);
                    }
                }

            }
            return obj;
        }

        public async Task<bool> Save(Employee model)
        {
            var result = false;
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_EMPLOYEE", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 30).Value = "INSERT";
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = model.id;
                cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar, 128).Value = model.name;
                cmd.Parameters.Add("@DOCUMENT_NUMBER", System.Data.SqlDbType.VarChar, 64).Value = model.document_number;
                cmd.Parameters.Add("@SALARY", System.Data.SqlDbType.Money).Value = model.salary;
                cmd.Parameters.Add("@AGE", System.Data.SqlDbType.Int).Value = model.age;
                cmd.Parameters.Add("@PROFILE", System.Data.SqlDbType.VarChar, 64).Value = model.profile;
                cmd.Parameters.Add("@ADMISSION_DATE", System.Data.SqlDbType.Date).Value = model.admission_date;

                var row = cmd.ExecuteNonQuery();
                if (row > 0)
                    result = true;

            }
            return result;

        }

        public async Task<bool> Update(Employee model)
        {
            var result = false;
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_EMPLOYEE", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 30).Value = "UPDATE";
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.BigInt).Value = model.id;
                cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar, 128).Value = model.name;
                cmd.Parameters.Add("@DOCUMENT_NUMBER", System.Data.SqlDbType.VarChar, 64).Value = model.document_number;
                cmd.Parameters.Add("@SALARY", System.Data.SqlDbType.Money).Value = model.salary;
                cmd.Parameters.Add("@AGE", System.Data.SqlDbType.Int).Value = model.age;
                cmd.Parameters.Add("@PROFILE", System.Data.SqlDbType.VarChar, 64).Value = model.profile;
                cmd.Parameters.Add("@ADMISSION_DATE", System.Data.SqlDbType.Date).Value = model.admission_date;

                var row = cmd.ExecuteNonQuery();
                if (row > 0)
                    result = true;

            }
            return result;
        }
    }
}
