using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace eCommerceSite.Controllers
{
    public class MusicInstrumentsController : Controller
    {
        private readonly MusicInstrumentContext

        public MusicInstrumentsController()
        {
                
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MusicInstrument newMusIns) 
        {
            if (ModelState.IsValid) 
            {
                // Add to DB
                // success message
                return View();
            }

            return View(newMusIns);

        }
    }
}
