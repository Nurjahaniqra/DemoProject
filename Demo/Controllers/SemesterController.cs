using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class SemesterController : Controller
    {
		public readonly ApplicationDbContext _Context;
		public SemesterController(ApplicationDbContext Context)
		{
			_Context = Context;
		}
		public IActionResult Index(int? pageNumber)
        {
			int pageSize = 2;
			var data = _Context.Semesters;
            return View(PageInformation<Semester>.Create(data, pageNumber ?? 1, pageSize));
        }
		[HttpGet]
		public IActionResult Create()
		{		
			return View();
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Create(Semester model)
		{

			if (ModelState.IsValid)
			{
				_Context.Semesters.Add(model);
				_Context.SaveChanges();
				return RedirectToAction("Index");

			}
			
			return View(model);
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var model = _Context.Semesters.Where(i => i.ID == id).FirstOrDefault();
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Edit(Semester model)
		{
			var data = _Context.Semesters.Where(i => i.ID == model.ID).FirstOrDefault();
			if (data == null)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				data.Name = model.Name;
				_Context.Update(data);
				_Context.SaveChanges();
				return RedirectToAction("Index");
			}
				return View(model);						
			
		}
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var model = _Context.Semesters.Where(u => u.ID == id).FirstOrDefault();
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Delete(Semester model)
		{

			var data = _Context.Semesters.Where(i => i.ID == model.ID).FirstOrDefault();
			if (data == null)
			{
				return NotFound();
			}		
			
			_Context.Semesters.Remove(data);
			_Context.SaveChanges();
			return RedirectToAction("Index");
		}



	}
}
