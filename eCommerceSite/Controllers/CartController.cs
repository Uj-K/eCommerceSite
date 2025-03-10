using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly MusicInstrumentContext _context;

        public CartController(MusicInstrumentContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            MusicInstrument? prodToAdd = _context.MusicInstruments.Where(p => p.InstrumentID == id).SingleOrDefault();
                                                                                                // 처음에는 FirstOrDefualt()로 했는데, 그럼 여러개 있어도 처음꺼만 가져오는거니까 Single 로 바꿈
            if (prodToAdd == null)
            {
                // Product with specified id does not exist
                TempData["Message"] = "Product was not found!";
                return RedirectToAction("Index", "MusicInstruments");
            }
            // Todo: Add item to cart cookie
            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "MusicInstruments");
        }
    }
}
