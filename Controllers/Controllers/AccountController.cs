using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;

namespace Portfolio.Web.Controllers
{
    public class AccountController(PortfolioContext context) : Controller
    {
        [HttpGet]
        public IActionResult Edit(int id = 1)
        {
            var admin = context.Users.FirstOrDefault(u => u.UserId == id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        public IActionResult Edit(int id, string userName, string currentPassword, string? newPassword, string? confirmPassword)
        {
            // DB'den admini bul
            var admin = context.Users.FirstOrDefault(u => u.UserId == id);
            if (admin == null)
            {
                return NotFound();
            }

            // Kullanıcı adını her durumda güncelle
            admin.UserName = userName;

            // Eğer şifre değiştirmek istiyorsa
            if (!string.IsNullOrEmpty(newPassword))
            {
                // 1) Mevcut şifre doğru mu?
                if (admin.Password != currentPassword)
                {
                    ModelState.AddModelError("", "Mevcut şifre yanlış!");
                    return View(admin);
                }

                // 2) Yeni şifre ile tekrar aynı mı?
                if (newPassword != confirmPassword)
                {
                    ModelState.AddModelError("", "Yeni şifre ile doğrulama şifresi eşleşmiyor!");
                    return View(admin);
                }

                // 3) Tüm kontroller doğruysa yeni şifreyi kaydet
                admin.Password = newPassword;
            }

            // DB'ye kaydet
            context.SaveChanges();
            ViewBag.Message = "Profil başarıyla güncellendi.";

            return View(admin);
        }


    }
}

