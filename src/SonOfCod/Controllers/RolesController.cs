using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using SonOfCod.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SonOfCod.Controllers
{
    public class RolesController : Controller
    {
        private readonly SonOfCodDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public RolesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SonOfCodDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            role.NormalizedName = role.Name.ToUpper();
            _db.Roles.Add(role);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string roleName)
        {
            IdentityRole role = _db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IdentityRole role)
        {
            role.NormalizedName = role.Name.ToUpper();
            _db.Entry(role).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string roleName)
        {
            IdentityRole role = _db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _db.Roles.Remove(role);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            List<SelectListItem> list = _db.Roles.OrderBy(r => r.Name)
                .ToList()
                .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name })
                .ToList();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(string userName, string roleName)
        {
            ApplicationUser user = _db.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            await _userManager.AddToRoleAsync(user, roleName);
            List<SelectListItem> list = _db.Roles.OrderBy(r => r.Name)
                .ToList()
                .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name })
                .ToList();
            ViewBag.Roles = list;
            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRoleFromUser(string userName, string roleName)
        {
            ApplicationUser user = _db.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            bool isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (isInRole)
            {
                ViewBag.ResultMessage = "Role removed from user.";
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            else
            {
                ViewBag.ResultMessage = "User is not assigned to selected role.";
            }
            List<SelectListItem> list = _db.Roles.OrderBy(r => r.Name)
                .ToList()
                .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name })
                .ToList();
            ViewBag.Roles = list;
            return View("ManageUserRoles");
        }
    }
}
