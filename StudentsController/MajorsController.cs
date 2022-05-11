using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsController {
    
    public class MajorsController {

        public string ConnectionString { get; set; }
        public SqlConnection SqlConnection { get; set; }

        public List<Major> GetAllMajors() {
            // check that the connection is established
            var majors = new List<Major>();
            var sql = "SELECT * from Major ";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var major = new Major();
                major.Id = Convert.ToInt32(reader["Id"]);
                major.Code = Convert.ToString(reader["Code"]);
                major.Description = Convert.ToString(reader["Description"]);
                major.MinSAT = Convert.ToInt32(reader["MinSAT"]);

                majors.Add(major);
            }

            reader.Close();

            return majors;
        }


        public void OpenConnection() {
            SqlConnection = new SqlConnection(ConnectionString);
            SqlConnection.Open();
            if (SqlConnection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
        }

        public void CloseConnection() {
            SqlConnection.Close();
        }

        public MajorsController(string ServerInstance, string Database) {
            ConnectionString = $"server={ServerInstance};"
                                + $"database={Database};"
                                + "TrustServerCertificate=True;"
                                + "trusted_connection=true;";
        }
    }
}
