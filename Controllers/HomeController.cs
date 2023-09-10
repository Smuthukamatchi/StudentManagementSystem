using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Data;
using StudentManagement.Models;
using Microsoft.AspNetCore.Http;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");
        }


       [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var details = _dbContext.Identities.FirstOrDefault(e => e.Name == username && e.Password == password);
            if (details != null)
            {
                HttpContext.Session.SetString("UserName", details.Name);

                if (details.Designation == "Teacher")
                {
                    return RedirectToAction("StaffDashboard", "Staff");
                }
                else if (details.Designation == "Principal")
                {
                    return RedirectToAction("PrincipalDashboard", "Staff");
                }
                else
                {
                    return RedirectToAction("StudentDashboard");
                }
            }
            TempData["ErrorMessage"] = "Login failed. Please enter valid credentials.";
            return RedirectToAction("Login");
        }

       public IActionResult StudentDashboard()
        {
            string studentName = HttpContext.Session.GetString("UserName");
            ViewData["StudentName"] = studentName;
            
            return View("~/Views/Student/StudentDashboard.cshtml");
        }

        [HttpGet]
        public IActionResult StudentMarks()
        {
            string studentName = HttpContext.Session.GetString("UserName");
            var student = _dbContext.Identities.FirstOrDefault(s => s.Name == studentName);

            if (student != null)
            {
                var studentMarks = _dbContext.Marks.FirstOrDefault(m => m.ID == student.ID);
                if (studentMarks != null)
                {
                    return View("~/Views/Student/StudentMarks.cshtml", studentMarks);
                }
            }

            TempData["ErrorMessage"] = "Student marks not found.";
            return RedirectToAction("StudentDashboard");
        }




[HttpGet]
public IActionResult StudentRank()
{
    string studentName = HttpContext.Session.GetString("UserName");

    // Console.WriteLine($"Staff Name from Session: {staffName}"); // Add this line for debugging

    var student = _dbContext.Identities.FirstOrDefault(s => s.Name == studentName && s.Designation == "Student");
    if (student == null)
    {
        TempData["Error"] = "Staff not found or not authorized.";
        return RedirectToAction("StudentDashboard");
    }

    List<Mark> students = _dbContext.Marks
        .Where(s => s.Class == student.Class)
        .OrderByDescending(s => s.Total) // Order students by total marks in descending order
        .ToList();

    if (students.Count == 0)
    {
        TempData["SearchError"] = "No students found in the same class.";
    }

    return View("~/Views/Student/StudentRank.cshtml", students);
}



       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


