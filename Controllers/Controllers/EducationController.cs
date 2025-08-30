using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    public class EducationController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var education = context.Educations
                .OrderByDescending(x => x.StartYear)
                .ToList();
            return View(education);
        }
        public IActionResult CreateEducation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEducation(Education education)
        {
            context.Add(education);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateEducation(int id)
        {
            var educations = context.Educations.Find(id);
            return View(educations);
        }
        [HttpPost]
        public IActionResult UpdateEducation(Education education)
        {
            context.Update(education);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteEducation(int id)
        {
            var education = context.Educations.Find(id);
            context.Remove(education);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
