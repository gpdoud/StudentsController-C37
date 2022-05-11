using Microsoft.Data.SqlClient;

namespace StudentsController {

    public class StudentController {

        public string ConnectionString { get; set; }
        public SqlConnection SqlConnection { get; set; }

        public Student? GetStudentByPk(int studentId) {
            // check that the connection is established
            var sql = "SELECT *, m.Id 'MajorPK' from Student s "
                        + " left join Major m on m.Id = s.MajorId "
                        + $" where s.Id = {studentId};";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            if (!reader.HasRows) {
                reader.Close();
                SqlConnection.Close();
                return null;
            }
            var student = new Student();
            student.Id = Convert.ToInt32(reader["Id"]);
            student.Firstname = Convert.ToString(reader["Firstname"]);
            student.Lastname = Convert.ToString(reader["Lastname"]);
            student.StateCode = Convert.ToString(reader["StateCode"]);
            student.SAT = (reader["SAT"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToDecimal(reader["GPA"]);
            student.MajorId = (reader["MajorId"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["MajorId"]);

            if(student.MajorId is null) {
                student.Major = null;
                reader.Close();
                SqlConnection.Close();
                return student;
            }
            var major = new Major();
            major.Id = Convert.ToInt32(reader["MajorPK"]);
            major.Code = Convert.ToString(reader["Code"]);
            major.Description = Convert.ToString(reader["Description"]);
            major.MinSAT = Convert.ToInt32(reader["MinSAT"]);

            student.Major = major;



            reader.Close();
           

            return student;
        }

        public List<Student> GetAllStudents() {
            // check that the connection is established
            var students = new List<Student>(65);
            var sql = "SELECT *, s.id 'StudentPK', m.Id 'MajorPK' from Student s "
                        + " join Major m on m.Id = s.MajorId; ";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["StudentPK"]);
                student.Firstname = Convert.ToString(reader["Firstname"]);
                student.Lastname = Convert.ToString(reader["Lastname"]);
                student.StateCode = Convert.ToString(reader["StateCode"]);
                student.SAT = (reader["SAT"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDecimal(reader["GPA"]);
                student.MajorId = (reader["MajorId"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["MajorId"]);

                var major = new Major();
                major.Id = Convert.ToInt32(reader["MajorPK"]);
                major.Code = Convert.ToString(reader["Code"]);
                major.Description = Convert.ToString(reader["Description"]);
                major.MinSAT = Convert.ToInt32(reader["MinSAT"]);

                student.Major = major;

                students.Add(student);
            }

            reader.Close();
           

            return students;
        }

        public bool RemoveStudent(Student student) {
            // check that the connection is established
            var sql = "DELETE Student "
                        + $" Where Id = {student.Id};";
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1) {
                throw new Exception("Remove failed!");
            }
            return true;
        }

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
            if (rowsAffected != 1) {
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