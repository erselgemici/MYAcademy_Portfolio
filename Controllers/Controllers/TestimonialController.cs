using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    public class TestimonialController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var references = context.Testimonials
                .OrderByDescending(x=>x.Review)
                .ToList();
            return View(references);
        }
        public IActionResult CreateTestimonial()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial testimonial)
        {
            context.Add(testimonial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateTestimonial(int id)
        {
            var reference = context.Testimonials.Find(id);
            return View(reference);
        }
        [HttpPost]
        public IActionResult UpdateTestimonial(Testimonial testimonial)
        {
            context.Update(testimonial);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteTestimonial(int id)
        {
            var reference = context.Testimonials.Find(id);
            context.Remove(reference);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
