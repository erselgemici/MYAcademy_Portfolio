using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    [AllowAnonymous]
    public class DefaultController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SendMessage(UserMessage message)
        {
            context.UserMessages.Add(message);
            context.SaveChanges();
            Task.Delay(1000);
            return RedirectToAction("Index");
        }
    }
}
