using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;

namespace Portfolio.Web.Controllers
{

    public class StatisticsController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
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

            ViewBag.reviewAverage = context.Testimonials.Any() ?
                context.Testimonials.Average(x => x.Review).ToString("0.0") : "Değerlendirme Yapılmadı";

            ViewBag.maxReviewOwner = context.Testimonials
                .OrderByDescending(x => x.Review)
                .Select(x => x.Name).FirstOrDefault();

            ViewBag.topSkill = context.Skills
                .OrderByDescending(x => x.Percentage)
                .Select(x => x.Title)
                .FirstOrDefault();

            ViewBag.latestProject = context.Projects
                .OrderByDescending(x => x.ProjectId)
                .Select(x => x.ProjectName)
                .FirstOrDefault();

            ViewBag.categoryCount = context.Categories.Count();

            ViewBag.referenceCount = context.Testimonials.Count();

            return View();
        }
    }
}
