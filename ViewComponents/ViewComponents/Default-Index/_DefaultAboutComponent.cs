using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;

namespace Portfolio.Web.ViewComponents.Default_Index
{
    public class _DefaultAboutComponent(PortfolioContext context) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var about = context.Abouts.FirstOrDefault();
            if (about != null)
            {
                var today = DateTime.Today;
                var age = today.Year - about.BirthDate.Year;
                if (about.BirthDate.Date > today.AddYears(-age)) age--;

                ViewBag.Age = age;
            }
            return View(about);
        }
    }
}
