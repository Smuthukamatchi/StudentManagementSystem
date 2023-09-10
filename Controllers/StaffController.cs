using System;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;
using StudentManagement.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace StudentManagement.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public StaffController(ILogger<StaffController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

       
        public IActionResult StaffDashboard()
        {
            string staffName = HttpContext.Session.GetString("UserName");
            ViewData["StaffName"] = staffName;
            return View("~/Views/Staff/StaffDashboard.cshtml");
        }
        
        public IActionResult PrincipalDashboard()
        {
            string staffName = HttpContext.Session.GetString("UserName");
            ViewData["StaffName"] = staffName;
            return View("~/Views/Staff/PrincipalDashboard.cshtml");
        }

       [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Identity student)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Identities.Add(student);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = " Added successfully.";

                return RedirectToAction("AddStudent");
            }

           return View("~/Views/Staff/AddStudent.cshtml", student);
        }


        [HttpGet]
        public IActionResult ViewStudents()
        {
            string staffName = HttpContext.Session.GetString("UserName");

            // Console.WriteLine($"Staff Name from Session: {staffName}"); // Add this line for debugging

            var staff = _dbContext.Identities.FirstOrDefault(s => s.Name == staffName && s.Designation == "Teacher");
            if (staff == null)
            {
                TempData["Error"] = "Staff not found or not authorized.";
                return RedirectToAction("StaffDashboard");
            }

            List<Identity> students = _dbContext.Identities
                .Where(s => s.Class == staff.Class && s.Designation == "Student")
                .ToList();

            if (students.Count == 0)
            {
                TempData["SearchError"] = "No students found in the same class.";
            }

            return View("~/Views/Staff/ViewStudents.cshtml", students);
        }

        [HttpGet]
        public IActionResult ViewStudentDetails(int id)
        {
            var student = _dbContext.Identities.FirstOrDefault(e => e.ID == id);

            if (student != null)
            {
                return View("~/Views/Staff/ViewStudentDetails.cshtml", student);
            }
            else
            {
                return RedirectToAction("ViewStudents");
            }
        }


        [HttpGet]
        public IActionResult EditProfile(int studentId)
        {
            var student = _dbContext.Identities.FirstOrDefault(e => e.ID == studentId);

            if (student != null)
            {
                return View("~/Views/Staff/EditProfile.cshtml", student);
            }

            TempData["ErrorMessage"] = "Student not found.";
            return RedirectToAction("StaffDashboard");
        }


        [HttpPost]
        public IActionResult UpdateProfile(Identity updateStudent)
        {
            if (ModelState.IsValid)
            {
                var student = _dbContext.Identities.FirstOrDefault(e => e.ID == updateStudent.ID);

                if (student != null)
                {
                    // Update all properties including the Password
                    student.Name = updateStudent.Name;
                    student.Email = updateStudent.Email;
                    student.Class = updateStudent.Class;
                    student.ContactNumber = updateStudent.ContactNumber;
                    student.Area = updateStudent.Area;
                    student.BloodGroup = updateStudent.BloodGroup;
                    student.Designation = updateStudent.Designation;
                    student.DateofBirth = updateStudent.DateofBirth;
                    student.Password = updateStudent.Password; // Update the password

                    _dbContext.SaveChanges();

                    TempData["SuccessMessage"] = "Profile updated successfully.";
                    return RedirectToAction("ViewStudents");
                }
            }

            TempData["SuccessMessage"] = "Failed to update profile.";
            return RedirectToAction("Viewstudents");
        }


        [HttpPost]
        public IActionResult DeleteProfile(int studentId)
        {
            var student = _dbContext.Identities.FirstOrDefault(e => e.ID == studentId);

            if (student != null)
            {
                _dbContext.Identities.Remove(student);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Student deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Student not found.";
            }

            return RedirectToAction("ViewStudents");
        }

        [HttpGet]
        public IActionResult AddSubjects()
        {
            return View("~/Views/Staff/AddSubjects.cshtml");
        }

        [HttpPost]
        public IActionResult AddSubjects(Subject student)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Subjects.Add(student);
                _dbContext.SaveChanges();
                
                TempData["SuccessMessage"] = "Subjects added successfully!";
                return RedirectToAction("AddSubjects"); // Redirect to the add subjects page
            }

           return View("~/Views/Staff/AddSubjects.cshtml");
        }

        [HttpGet]
        public IActionResult ViewSubjects(string searchClass)
        {
            List<Subject> subjects;

            if (_dbContext.Subjects != null)
            {
                if (string.IsNullOrEmpty(searchClass))
                {
                    subjects = _dbContext.Subjects.ToList();
                }
                else
                {
                    subjects = _dbContext.Subjects
                        .AsEnumerable()
                        .Where(s => !string.IsNullOrEmpty(s.Class) && 
                                    s.Class.Contains(searchClass, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (subjects.Count == 0)
                    {
                        TempData["SearchError"] = "No subjects found for the entered class.";
                    }
                }
            }
            else
            {
                subjects = new List<Subject>(); // Initialize with an empty list to avoid null reference
            }

            return View("~/Views/Staff/ViewSubjects.cshtml", subjects);
        }


        [HttpGet]
        public IActionResult EditSubject(int subjectId)
        {
            var subject = _dbContext.Subjects.FirstOrDefault(s => s.subjectId == subjectId);

            if (subject != null)
            {
                return View("~/Views/Staff/EditSubject.cshtml", subject);
            }

            TempData["ErrorMessage"] = "Subject not found.";
            return RedirectToAction("ViewSubjects");
        }


        [HttpPost]
        public IActionResult UpdateSubject(Subject updatedSubject)
        {
            if (ModelState.IsValid)
            {
                var subject = _dbContext.Subjects.FirstOrDefault(s => s.subjectId == updatedSubject.subjectId);

                if (subject != null)
                {
                    // Update subject details
                    subject.Subject1 = updatedSubject.Subject1;
                    subject.Subject2 = updatedSubject.Subject2;
                    subject.Subject3 = updatedSubject.Subject3;
                    subject.Subject4 = updatedSubject.Subject4;
                    subject.Subject5 = updatedSubject.Subject5;
                    subject.Subject6 = updatedSubject.Subject6;

                    _dbContext.SaveChanges();

                    TempData["SuccessMessage"] = "Subject updated successfully.";
                    return RedirectToAction("ViewSubjects");
                }
            }

            TempData["ErrorMessage"] = "Failed to update subject.";
            return RedirectToAction("ViewSubjects");
        }




        [HttpPost]
        public IActionResult DeleteSubject(int subjectId)
        {
            var subject = _dbContext.Subjects.FirstOrDefault(s => s.subjectId == subjectId);

            if (subject != null)
            {
                _dbContext.Subjects.Remove(subject);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Subject deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Subject not found.";
            }

            return RedirectToAction("ViewSubjects");
        }


        [HttpGet]
        public IActionResult AddMarks()
        {
            string staffName = HttpContext.Session.GetString("UserName");

            var staff = _dbContext.Identities.FirstOrDefault(s => s.Name == staffName && s.Designation == "Teacher");
            if (staff == null)
            {
                TempData["Error"] = "Staff not found or not authorized.";
                return RedirectToAction("StaffDashboard");
            }

            List<Identity> students = _dbContext.Identities
                .Where(s => s.Class == staff.Class && s.Designation == "Student")
                .ToList();

            if (students.Count == 0)
            {
                TempData["SearchError"] = "No students found in the same class.";
            }

            return View("~/Views/Staff/AddMarks.cshtml", students);
        }

        [HttpGet]
        public IActionResult Marks(int id, string @class)
        {
            ViewData["ID"] = id;
            ViewData["Class"] = @class;
            var name = _dbContext.Identities.FirstOrDefault(s => s.ID == id);
            if (name != null)
            {
                ViewData["Name"] = name.Name; // Assuming 'Name' is the property in the Identities model that holds the name value
            }


            Subject subject = _dbContext.Subjects.FirstOrDefault(s => s.Class == @class );

            if (subject == null)
            {
                // Handle the case where the subjects for the class are not found
                return View("NoSubjectsFound"); // Create a view named "NoSubjectsFound.cshtml"
            }

            return View(subject);
        }


        [HttpPost]
        public IActionResult SaveMarks(Mark marks)
        {
            if (ModelState.IsValid)
            {
                Mark newMark = new Mark
                {
                    ID = marks.ID,
                    Class = marks.Class,
                    Name = marks.Name,
                    Subject1 = marks.Subject1,
                    Subject2 = marks.Subject2,
                    Subject3 = marks.Subject3,
                    Subject4 = marks.Subject4,
                    Subject5 = marks.Subject5,
                    Subject6 = marks.Subject6,
                    Subject1Name = marks.Subject1Name,
                    Subject2Name = marks.Subject2Name,
                    Subject3Name = marks.Subject3Name,
                    Subject4Name = marks.Subject4Name,
                    Subject5Name = marks.Subject5Name,
                    Subject6Name = marks.Subject6Name,

                    Total = CalculateTotalMarks(
                        int.Parse(marks.Subject1),
                        int.Parse(marks.Subject2),
                        int.Parse(marks.Subject3),
                        int.Parse(marks.Subject4),
                        int.Parse(marks.Subject5),
                        int.Parse(marks.Subject6))
                };

                _dbContext.Marks.Add(newMark);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Marks saved successfully.";
                return RedirectToAction("AddMarks"); // Redirect to the appropriate action
            }

            TempData["SuccessMessage"] = "Failed to save marks.";
            return RedirectToAction("AddMarks"); // Redirect to the appropriate action
        }

        private int CalculateTotalMarks(params int[] marks)
        {
            int total = 0;
            foreach (int mark in marks)
            {
                total += mark;
            }
            return total;
        }

        [HttpGet]
        public IActionResult ViewMarks(int id, string name)
        {
            var studentMark = _dbContext.Marks.FirstOrDefault(m => m.ID == id && m.Name == name);
            if (studentMark != null)
            {
                return View("~/Views/Staff/ViewMarks.cshtml", studentMark);
            }
            return RedirectToAction("AddMarks");
        }




        [HttpGet]
        public IActionResult EditMarks(int studentId)
        {
            var marks = _dbContext.Marks.FirstOrDefault(m => m.ID == studentId);

            if (marks != null)
            {
                return View("~/Views/Staff/EditMarks.cshtml", marks);
            }

            TempData["ErrorMessage"] = "Student marks not found.";
            return RedirectToAction("AddMarks");
        }

        [HttpPost]
        public IActionResult UpdateMarks(Mark updatedMarks)
        {
            if (ModelState.IsValid)
            {
                var marks = _dbContext.Marks.FirstOrDefault(m => m.ID == updatedMarks.ID);

                if (marks != null)
                {
                    // Update marks for different subjects
                    marks.Subject1 = updatedMarks.Subject1;
                    marks.Subject2 = updatedMarks.Subject2;
                    marks.Subject3 = updatedMarks.Subject3;
                    marks.Subject4 = updatedMarks.Subject4;
                    marks.Subject5 = updatedMarks.Subject5;
                    marks.Subject6 = updatedMarks.Subject6;
                marks.Total = CalculateTotalMarks(
                            int.Parse(marks.Subject1),
                            int.Parse(marks.Subject2),
                            int.Parse(marks.Subject3),
                            int.Parse(marks.Subject4),
                            int.Parse(marks.Subject5),
                            int.Parse(marks.Subject6));
                    // ... Update other subject marks

                    _dbContext.SaveChanges();

                    TempData["SuccessMessage"] = "Student marks updated successfully.";
                    return RedirectToAction("AddMarks");
                }
            }

            TempData["ErrorMessage"] = "Failed to update student marks.";
            return RedirectToAction("AddMarks");
        }

        [HttpPost]
        public IActionResult DeleteMarks(int studentId)
        {
            var marks = _dbContext.Marks.FirstOrDefault(m => m.ID == studentId);

            if (marks != null)
            {
                _dbContext.Marks.Remove(marks);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Student marks deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Student marks not found.";
            }

            return RedirectToAction("AddMarks");
        }


[HttpGet]
public IActionResult ViewRank()
{
    string staffName = HttpContext.Session.GetString("UserName");

    // Console.WriteLine($"Staff Name from Session: {staffName}"); // Add this line for debugging

    var staff = _dbContext.Identities.FirstOrDefault(s => s.Name == staffName && s.Designation == "Teacher");
    if (staff == null)
    {
        TempData["Error"] = "Staff not found or not authorized.";
        return RedirectToAction("StaffDashboard");
    }

    List<Mark> students = _dbContext.Marks
        .Where(s => s.Class == staff.Class)
        .OrderByDescending(s => s.Total) // Order students by total marks in descending order
        .ToList();

    if (students.Count == 0)
    {
        TempData["SearchError"] = "No students found in the same class.";
    }

    return View("~/Views/Staff/ViewRank.cshtml", students);
}





    }

}