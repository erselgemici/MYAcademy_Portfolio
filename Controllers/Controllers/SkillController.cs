using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    public class SkillController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var skills = context.Skills
                .OrderByDescending(x=>x.Percentage)
                .ToList();
            return View(skills);
        }
        public IActionResult CreateSkill()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSkill(Skill skill)
        {
            context.Skills.Add(skill);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteSkill(int id)
        {
            var skill = context.Skills.Find(id);
            context.Remove(skill);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateSkill(int id)
        {
            var skill = context.Skills.Find(id);
            return View(skill);
        }
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            context.Update(skill);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
