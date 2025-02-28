using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
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
                // Map RegisterViewModel to Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
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
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found!");

            }
            // Return page if no record found, or ModelState is invalid
            return View(loginModel);
        }
    }
}
