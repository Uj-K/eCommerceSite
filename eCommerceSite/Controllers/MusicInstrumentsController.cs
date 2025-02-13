using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace eCommerceSite.Controllers
{
    public class MusicInstrumentsController : Controller
    {
        private readonly MusicInstrumentContext _context;

        public MusicInstrumentsController(MusicInstrumentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(MusicInstrument newMusIns) 
        {
            if (ModelState.IsValid) 
            {
                _context.MusicInstruments.Add(newMusIns); //Prepares insert
                await _context.SaveChangesAsync();        // Executes pending insert

                ViewData["Message"] = $"{newMusIns.Title} was added successfully!";

                return View();
            }

            return View(newMusIns);

        }
    }
}
