using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class MusicInstrumentsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
