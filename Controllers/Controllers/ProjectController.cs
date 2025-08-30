using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    public class ProjectController(PortfolioContext context) : Controller
    {
        private void CategoryDropdown()
        {
            var categories = context.Categories.ToList();
            ViewBag.categories = (from x in categories
                                  select new SelectListItem
                                  {
                                      Text = x.CategoryName,
                                      Value = x.CategoryId.ToString()
                                  }).ToList();
        }
        public IActionResult Index()
        {
            //Eager Loading
            //Core'da ilişkili tabloyu direkt olarak listeleyemiyoruz. Include etmemiz gerekiyor.
            var projects = context.Projects.Include(x=>x.Category).ToList();
            return View(projects);
        }

        public IActionResult CreateProject()
        {
            CategoryDropdown();
            return View();
        }
        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            CategoryDropdown();
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            context.Projects.Add(project);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateProject(int id)
        {
            CategoryDropdown();
            var project = context.Projects.Find(id);
            return View(project);
        }
        [HttpPost]
        public IActionResult UpdateProject(Project p)
        {
            CategoryDropdown();
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            context.Projects.Update(p);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProject(int id)
        {
            var project = context.Projects.Find(id);
            context.Projects.Remove(project);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
