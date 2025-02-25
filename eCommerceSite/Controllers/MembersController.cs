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
    }
}
