using Microsoft.Data.SqlClient;

namespace StudentsController {
    
    public class StudentController {

        public string ConnectionString { get; set; }
        public SqlConnection SqlConnection { get; set; }

        public bool ChangeStudent(Student student) {
            // check that the connection is established
            var sql = "UPDATE Student SET "
                       + $" Firstname = '{student.Firstname}', "
                        + $" Lastname = '{student.Lastname}', "
                        + $" StateCode = '{student.StateCode}', "
                        + $" SAT = {student.SAT}, "
                        + $" GPA = {student.GPA}, "
                        + $" MajorId = {student.MajorId} "
                        + $" Where Id = {student.Id};";
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1) {
                throw new Exception("Insert failed!");
            }
            return true;
        }

        public bool AddStudent(Student student) {
            // check that the connection is established
            var sql = "INSERT Student "
                       + " (Firstname, Lastname, StateCode, SAT, GPA, MajorId) "
                       + " VALUES "
                       + $" ('{student.Firstname}', '{student.Lastname}', "
                       + $" '{student.StateCode}', {student.SAT}, {student.GPA}, {student.MajorId});";
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected != 1) {
                throw new Exception("Insert failed!");
            }
            return true;
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

        public StudentController(string ServerInstance, string Database) {
            ConnectionString = $"server={ServerInstance};"
                                + $"database={Database};"
                                + "TrustServerCertificate=True;"
                                + "trusted_connection=true;";
        }

    }
}