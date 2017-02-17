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
        private readonly SonOfCodDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public NewsletterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SonOfCodDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
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
            _db.MailingList.Add(newRecipient);
            _db.SaveChanges();
            return RedirectToAction("AddedToNewsletter");
        }

        public IActionResult AddedToNewsletter()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult MailingList()
        {
            List<ApplicationUser> mailingList = _db.MailingList.Include(ml => ml.AppUser)
                .ToList()
                .Select(ml => ml.AppUser)
                .ToList();
            return View(mailingList);
        }
    }
}
