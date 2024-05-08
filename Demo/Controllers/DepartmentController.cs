using Demo.Data;
using Demo.Models.ViewModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace Demo.Controllers
{
    public class DepartmentController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public DepartmentController(ApplicationDbContext Context)
        {
            _Context = Context;            
        }

        public IActionResult Index(int? pageNumber)
        {
            int pageSize = 2;
            //var data=_Context.Departments.Where(i=>i.IsDelete==0).ToList();
            return View(PageInformation<Department>.Create(_Context.Departments.Where(i => i.IsDelete == 0),pageNumber ?? 1,pageSize));
        }

       

		[HttpGet]
        public IActionResult Create()
        {
            return View();
        }
	

		[HttpPost]
        public IActionResult Create(Department model)
        {
            if(_Context.Departments.Any(i=>i.Code==model.Code))
            {
                ModelState.AddModelError("Code", "Code must be Unique.");  
            }
            if (_Context.Departments.Any(i => i.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Name must be Unique.");
            }
            if(ModelState.IsValid)
            {
                _Context.Departments.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _Context.Departments.Where(i => i.ID == id).FirstOrDefault();
            if (model == null)
            {
                return NotFound(); 
            }
            return View(model);
        }

        [HttpPost]        
        public IActionResult Edit(Department model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var data = _Context.Departments.Where(i => i.ID == model.ID).FirstOrDefault();
            if (data == null)
            {
                return NotFound(); 
            }
       
            data.Code = model.Code;
            data.Name = model.Name;

			_Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
		public IActionResult Delete(int id)
		{
			var model = _Context.Departments.Where(u => u.ID == id).FirstOrDefault();			
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}
		[HttpPost]
		public IActionResult Delete(Department model)
        {
            
			var data = _Context.Departments.Where(i => i.ID == model.ID).FirstOrDefault();
			if (data == null)
			{
				return NotFound();
			}
            var IsDelete = 1;
			data.IsDelete = IsDelete;
			data.Code = model.Code;			
			data.Name = model.Name;	
			
			_Context.SaveChanges();
            return RedirectToAction("Index");
        }
  
    }
}

