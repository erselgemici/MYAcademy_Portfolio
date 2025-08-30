using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using System.Globalization;

namespace Portfolio.Web.ViewComponents.Default_Index
{
    public class _DefaultStatisticComponent(PortfolioContext context) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.projectCount = context.Projects.Count();

            ViewBag.skillAverage = context.Skills.Any() ?
                context.Skills.Average(x => x.Percentage).ToString("00.00") : 0.0.ToString("00.00");

            ViewBag.unreadMessageCount = context.UserMessages.Count(x => x.IsRead == false);

            ViewBag.lastMessageQwner = context.UserMessages
                .OrderByDescending(x => x.UserMessageId)
                .Select(x => x.Name).FirstOrDefault();

            var startYear = context.Experiences.Min(x => x.StartYear);
            ViewBag.experienceYear = DateTime.Now.Year - startYear;

            ViewBag.companyCount = context.Experiences
                .Select(x => x.Company)
                .Distinct().Count();

            //ViewBag.reviewAverage = context.Testimonials.Any() ?
            //    context.Testimonials.Average(x => x.Review).ToString("0.0") : "Değerlendirme Yapılmadı";
            var avg = context.Testimonials.Average(x => x.Review);
            ViewBag.reviewAverage = avg.ToString("0.0", CultureInfo.InvariantCulture);


            ViewBag.maxReviewOwner = context.Testimonials
                .OrderByDescending(x => x.Review)
                .Select(x => x.Name).FirstOrDefault();
            return View();
        }
    }
}
