using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.ViewComponents.AdminLayout
{
    public class _AdminLayoutTopbarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View();
        }
    }
}
