using StudentsController;

var studctrl = new StudentController("localhost\\sqlexpress", "EdDb");
studctrl.OpenConnection();

var student = new Student() {
    Id = 0, Firstname = "Graham", Lastname = "Kraker",
    StateCode = "OH", GPA = 3.0m, SAT = 1200, MajorId = 1 
};

//var rc = studctrl.AddStudent(student);

student.Id = 63;

var rc = studctrl.ChangeStudent(student); ;


studctrl.CloseConnection();