using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    public class ExperienceController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var exp = context.Experiences
                .OrderByDescending(x => x.StartYear)
                .ToList();
            return View(exp);
        }
        public IActionResult CreateExperience()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            context.Add(experience);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateExperience(int id)
        {
            var exp = context.Experiences.Find(id);
            return View(exp);
        }
        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            context.Update(experience);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteExperience(int id)
        {
            var exp = context.Experiences.Find(id);
            context.Remove(exp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
