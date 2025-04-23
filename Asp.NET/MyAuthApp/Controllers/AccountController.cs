using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAuthApp.Entities;
using MyAuthApp.Models;

namespace MyAuthApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.UserAccounts.ToList());
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Email = model.Email;
                account.Username = model.Username;
                account.Password = model.Password;

                try
                {
                    _context.UserAccounts.Add(account);
                    _context.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = $"{model.FirstName} {model.LastName}, your registration is successful!";
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Use unique Email and Username!");
                    return View(model);
                }

                return View();
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserAccounts.Where(x => (x.Username == model.UsernameOrEmail || x.Email == model.UsernameOrEmail) && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //successful login- create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    var claimsIndentiy = new ClaimsIdentity(claims, "LoginCookie");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIndentiy);
                    HttpContext.SignInAsync("LoginCookie",claimsPrincipal);
                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username/email or password.");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout() {
            //delete cookies
            HttpContext.SignOutAsync("LoginCookie");
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = User.Identity?.Name;
            ViewBag.Email = User.FindFirst(ClaimTypes.Email)?.Value;
            return View();
        }
    }
}
