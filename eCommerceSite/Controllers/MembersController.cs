using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly MusicInstrumentContext _context;
        public MembersController(MusicInstrumentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // Check if email is already in use
                if (await (from member in _context.Members
                          where member.Email == regModel.Email
                          select member).AnyAsync())
                {
                    // Add error message that will display next to the email field
                    ModelState.AddModelError(nameof(RegisterViewModel.Email), "Email is already in use!");
                    return View(regModel);
                }

                // Map RegisterViewModel to Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();

                LogUserIn(newMember.Email);

                return RedirectToAction("Index", "Home");

            }
            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check DB for credentials
                Member? m = (from memebr in _context.Members
                             where memebr.Email == loginModel.Email &&
                                   memebr.Password == loginModel.Password
                             select memebr).SingleOrDefault(); // 하나만 있어야 되니까

                // If exists, send to homepage
                if (m != null)
                {
                    LogUserIn(loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found!");

            }
            // Return page if no record found, or ModelState is invalid
            return View(loginModel);
        }

        private void LogUserIn(string email)
        {
            HttpContext.Session.SetString("Email", email);
        }

        [HttpGet]

        public IActionResult Logout() 
        { 
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
