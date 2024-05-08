using Demo.Data;
using Demo.Models;
using Demo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class CourseController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public CourseController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public IActionResult Index()
        {
            var data = _Context.Courses
				.Include(x => x.Department)
                .Include(x=>x.Semester).ToList();
            return View(data);
        }

		[HttpGet]
		public IActionResult GetCoursesByDepartment(int departmentId, int? pageNumber)
		{
			
			int pageSize = 2;
			var courses = _Context.Courses.AsQueryable();
			ViewBag.departmentId = departmentId;
			ViewBag.Departments = new SelectList(_Context.Departments, "ID", "Name", departmentId);
			if (departmentId!=0)
			{
				courses = courses.Where(c => c.DepartmentID == departmentId);
			}
			
			var result = courses.Select(c => new CourseViewModel
			{
				CourseID = c.ID,
				Code = c.Code,
				Name = c.Name,
				Credit = c.Credit,
				DepartmentName = c.Department.Name
			});
			
			return View(PageInformation<CourseViewModel>.Create(result, pageNumber ?? 1, pageSize));
		}

		[HttpGet]
        public IActionResult Create()
        {
			ViewBag.Departments = new SelectList(_Context.Departments, "ID", "Name");
			ViewBag.Semester = new SelectList(_Context.Semesters, "ID", "Name");
			return View();

        }

        [HttpPost]
        public IActionResult Create(Course model)
        {
			if (_Context.Courses.Any(i => i.Credit == model.Credit))
			{
				ModelState.AddModelError("Credit", "The field Credit must be between 0.5 and 5.");
			}
			if (_Context.Courses.Any(i=>i.Code==model.Code))
            {
                ModelState.AddModelError("Code","Code Must be Unique.");
            }
            if (_Context.Courses.Any(i => i.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Name Must be Unique.");
            }

                      

			if (!ModelState.IsValid)
            {
                _Context.Courses.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
			ViewBag.Departments = new SelectList(_Context.Departments, "ID", "Name");
			ViewBag.Semester = new SelectList(_Context.Semesters, "ID", "Name");
			var data = _Context.Courses.Where(i => i.ID == id).FirstOrDefault();
            if(data==null)
            {
                return NotFound();
            }
            return View(data);

        }
        [HttpPost]
        public IActionResult Edit(Course model)
        {
			ViewBag.Departments = new SelectList(_Context.Departments, "ID", "Name");
			ViewBag.Semester = new SelectList(_Context.Semesters, "ID", "Name");
			if(ModelState.IsValid)
			{
				return View(model);
			}
			var data = _Context.Courses.Where(i => i.ID == model.ID).FirstOrDefault();
						
					
					if (data == null)
					{
						return NotFound();
					}

					data.Code = model.Code;
					data.Name = model.Name;
					data.Credit = model.Credit;
					data.Description = model.Description;
					data.DepartmentID = model.DepartmentID;
					data.SemesterID = model.SemesterID;

					_Context.SaveChanges();
					return RedirectToAction("Index");				
						
			
		}
		[HttpGet]
		public IActionResult Delete(int id)
		{
			ViewBag.Departments = new SelectList(_Context.Departments, "ID", "Name");
			ViewBag.Semester = new SelectList(_Context.Semesters, "ID", "Name");
			var model = _Context.Courses.FirstOrDefault(u => u.ID == id);
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

		[HttpPost]
		public IActionResult Delete(Course model)
		{
			ViewBag.Departments = new SelectList(_Context.Departments, "ID", "Name");
			ViewBag.Semester = new SelectList(_Context.Semesters, "ID", "Name");
			var data = _Context.Courses.FirstOrDefault(i => i.ID == model.ID);
			if (data == null)
			{
				return NotFound();
			}				
			
			_Context.Courses.Remove(data);
			_Context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
