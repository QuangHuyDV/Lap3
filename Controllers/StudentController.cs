using Lap3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;


namespace Lap1.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {

        private List<Student> listStudents = new List<Student>();
        public StudentController()
        {
            listStudents = new List<Student>(){
                new Student() {Id = 1, Name = "Khanh Lai", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "Hà Nội", Email = "lai@gmail.com", Score = 10},
                new Student() {Id = 2, Name = "Hải Nam", Branch = Branch.CE, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@gmail.com", Score = 5.5},
                new Student() {Id = 3, Name = "Thanh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "A2-2018", Email = "tu@gmail.com", Score = 6.8},
                new Student() {Id = 4, Name = "Ngọc Long", Branch = Branch.IT, Gender = Gender.Male, IsRegular = false, Address = "A3-2018", Email = "long@gmail.com", Score = 1.2},
            };
        }

        [HttpGet("List")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet("Add")]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid){
                s.Id = listStudents.Last<Student>().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents);
            }
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>() {
                new SelectListItem() {Text = "IT", Value = "1"},
                new SelectListItem() {Text = "BE", Value = "2"},
                new SelectListItem() {Text = "CE", Value = "3"},
                new SelectListItem() {Text = "EE", Value = "4"}
            };
            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create(Student student, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }

                // Lưu đường dẫn ảnh vào thuộc tính ImagePath
                student.Image = "/uploads/" + fileName;
            }
            student.Id = listStudents.Count > 0 ? listStudents.Max(s => s.Id) + 1 : 1;
            listStudents.Add(student);
            return View("Index", listStudents);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
