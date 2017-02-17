using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SonOfCod.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SonOfCod.Controllers
{
    public class NewsletterController : Controller
    {
        private SonOfCodDbContext db = new SonOfCodDbContext();

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost, ActionName("Signup")]
        public IActionResult SignupUser()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            NewsletterRecipient newRecipient = new NewsletterRecipient() { };
            newRecipient.AppUserId = userId;
            db.MailingList.Add(newRecipient);
            db.SaveChanges();
            return RedirectToAction("AddedToNewsletter");
        }

        public IActionResult AddedToNewsletter()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult MailingList()
        {
            List<ApplicationUser> mailingList = db.MailingList.Include(ml => ml.AppUser)
                .ToList()
                .Select(ml => ml.AppUser)
                .ToList();
            return View(mailingList);
        }
    }
}
