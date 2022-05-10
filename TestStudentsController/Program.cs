using StudentsController;

var studctrl = new StudentController("localhost\\sqlexpress", "EdDb");
studctrl.OpenConnection();
var majctrl = new MajorsController("localhost\\sqlexpress", "EdDb");
majctrl.OpenConnection();

var majors = majctrl.GetAllMajors();

//var student = studctrl.GetStudentByPk(3);

//Console.WriteLine(student);

//var students = studctrl.GetAllStudents();

//var studentNameMajor = from s in students
//                       orderby s.Major.Description
//                       select new {
//                           s.Firstname, s.Lastname, Major = s.Major.Description
//                       };

//foreach(var s in studentNameMajor) {
//    Console.WriteLine($"{s.Firstname} {s.Lastname}; Major is  {s.Major}");
//}

//var studentWithHighestSAT = (from s in students
//                             orderby s.SAT descending
//                             select s).FirstOrDefault();

//Console.WriteLine(studentWithHighestSAT);


//var studentFromOhio = students.Where(s => s.StateCode == "OH" && s.GPA >= 3).OrderBy(s => s.Lastname);

//foreach (var student in studentFromOhio) {
//    Console.WriteLine(student);
//}

//studentFromOhio = from s in students
//                  where s.StateCode == "OH" && s.GPA >= 3
//                  orderby s.Lastname
//                  select s;

//foreach (var student in studentFromOhio) {
//    Console.WriteLine(student);
//}

//var student = new Student() {
//    Id = 0, Firstname = "Graham", Lastname = "Kraker",
//    StateCode = "OH", GPA = 3.0m, SAT = 1200, MajorId = 1 
//};

//var rc = studctrl.AddStudent(student);

//student.Id = 63;

//var rc = studctrl.ChangeStudent(student);

//rc = studctrl.RemoveStudent(63);

majctrl.CloseConnection();
studctrl.CloseConnection();