using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    public class ContactController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var contacts = context.ContactInfos.ToList();                
            return View(contacts);
        }
        public IActionResult CreateContactInfo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateContactInfo(ContactInfo contactInfo)
        {
            context.ContactInfos.Add(contactInfo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteContactInfo(int id)
        {
            var contactInfo = context.ContactInfos.Find(id);
            context.Remove(contactInfo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateContactInfo(int id)
        {
            var contactInfo = context.ContactInfos.Find(id);
            return View(contactInfo);
        }
        [HttpPost]
        public IActionResult UpdateContactInfo(ContactInfo contactInfo)
        {
            context.Update(contactInfo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
