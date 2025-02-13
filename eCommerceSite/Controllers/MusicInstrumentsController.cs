using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            // Get all data from the DB
            List<MusicInstrument> musicInstruments = await _context.MusicInstruments.ToListAsync();

            /*List<MusicInstrument> musicInstruments = (from musicInstruments in _context.MusicInstruments
             *(이렇게 쓸수도 있다)                        select musicInstruments).ToListAsync*/

            // show them on the page
            return View(musicInstruments);
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
