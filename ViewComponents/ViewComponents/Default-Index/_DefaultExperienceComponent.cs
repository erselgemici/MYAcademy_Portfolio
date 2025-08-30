using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;

namespace Portfolio.Web.ViewComponents.Default_Index
{
    public class _DefaultExperienceComponent(PortfolioContext context) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var education = context.Experiences
                .OrderByDescending(x => x.StartYear)
                .ToList();
            return View(education);
        }
    }
}
