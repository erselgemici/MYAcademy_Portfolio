using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;

namespace Portfolio.Web.Controllers
{
    public class MessageController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var userMessages = context.UserMessages
                .OrderByDescending(x => x.UserMessageId)
                .ToList();

            // Okunmamış mesajları layout için gönder
            ViewBag.UnreadMessages = context.UserMessages
                .Where(x => !x.IsRead)
                .OrderByDescending(x => x.UserMessageId)
                .Take(5)
                .ToList();

            ViewBag.UnreadCount = context.UserMessages.Count(x => !x.IsRead);

            return View(userMessages);
        }       
        public IActionResult MarkAsUnread(int id)
        {
            var msg = context.UserMessages.Find(id);
            if (msg != null)
            {
                msg.IsRead = false;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult MarkAsRead(int id)
        {
            var msg = context.UserMessages.Find(id);
            if (msg != null)
            {
                msg.IsRead = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult ReadMessages()
        {
            var userMessages = context.UserMessages
                .Where(x => x.IsRead == true)
                .OrderByDescending(x => x.UserMessageId)
                .ToList();
            return View("Index", userMessages);
        }
        public IActionResult UnreadMessages()
        {
            var userMessages = context.UserMessages
                .Where(x => x.IsRead == false)
                .OrderByDescending(x => x.UserMessageId)
                .ToList();
            return View("Index", userMessages);
        }
        public IActionResult DeleteMessage(int id)
        {
            var msg = context.UserMessages.Find(id);
            context.UserMessages.Remove(msg);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ReadMessage(int id)
        {
            var msg = context.UserMessages.Find(id);
            msg.IsRead = true;
            context.SaveChanges();
            return PartialView("_MessageDetail",msg);
        }
    }
}
