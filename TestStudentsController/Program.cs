using StudentsController;

const string server = "localhost\\sqlexpress";
const string database = "EdDB";

var studctrl = new StudentController(server, database);
studctrl.OpenConnection();
var majctrl = new MajorsController(server, database);
majctrl.OpenConnection();

var majors = majctrl.GetAllMajors();
var students = studctrl.GetAllStudents();

var studentsMajors = from s in students
                     join m in majors
                       on s.MajorId equals m.Id
                     where s.StateCode == "OH"
                     orderby s.Lastname descending
                     select new {
                         Fullname = s.Firstname + " " + s.Lastname, 
                         Major = m.Description
                     };

foreach(var sm in studentsMajors) {
    Console.WriteLine($"{sm.Fullname} | {sm.Major}");
}


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