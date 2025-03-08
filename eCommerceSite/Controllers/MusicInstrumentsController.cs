using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;

namespace eCommerceSite.Controllers
{
    public class MusicInstrumentsController : Controller
    {
        private readonly MusicInstrumentContext _context;

        public MusicInstrumentsController(MusicInstrumentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            /* 로그인 한 사람만 접근 가능하게 하려면 이렇게 하면 됨
             * if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Members");
            }
            근데 더 나은 방법 있다고 함. 다음학기에 배운다고 함*/

            const int NumProductsToDispalyPerpage = 3;
            const int PageOffset = 1; // Need a page offset to use current page and figure out.

            int currPage = id ?? 1; // Coalescing operator. Set currPage to id if it has a maxNumpages, otherwise set it to 1
            /* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            // int currPage = id.HasValue ? id.Value : 1; 이거 보다 더 짧게도 가능
            //         boolean condition ? 이다음 True면 일어나는 일 : false 면 일어나는 일 
            // 밑에 이거 이렇게 위처럼 한줄로 쓸수있음
            // if (id.HasValue)
            // {
            //      currPage = id.Value;
            // }
            // else
            // {
            //      currPage = 1;
            // }
            */

            int totalNumOfProducts = await _context.MusicInstruments.CountAsync();
            double maxNumpages = Math.Ceiling((double)totalNumOfProducts / NumProductsToDispalyPerpage); // 저 ceiling이 올림해준다.
            int lastPage = Convert.ToInt32(maxNumpages); // Rounding pages up, to next whole page number


            // Get all data from the DB
            /*(이렇게 쓸수도 있다)    
            List<MusicInstrument> musicInstruments = await _context.MusicInstruments
                                                    .Skip(NumProductsToDispalyPerpage * (currPage - PageOffset))
                                                    .Take(NumProductsToDispalyPerpage).ToListAsync();
            */

            List<MusicInstrument> musicInstruments = await (from musicInstrument in _context.MusicInstruments
                                                            select musicInstrument)
                                                            .Skip(NumProductsToDispalyPerpage * (currPage - PageOffset))
                                                            .Take(NumProductsToDispalyPerpage)
                                                            .ToListAsync();

            CatalogViewModel catalogModel = new(musicInstruments, lastPage, currPage);
            // show them on the page
            return View(catalogModel);
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


        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            MusicInstrument instToEdit = await _context.MusicInstruments.FindAsync(id);

            if (instToEdit == null) 
            { 
                return NotFound();
            }

            return View(instToEdit);
        }
        /* async는 비동기 코드는 작업을 실행하고 기다리는 동안 다른 작업을 수행할 수 있는 코드
         * 추가할때 async, Task<>, await 세개가 세트 인듯 */

        [HttpPost]
        public async Task<IActionResult> Edit(MusicInstrument instModel)
        {
            if (ModelState.IsValid)
            {
                _context.MusicInstruments.Update(instModel);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{instModel.Title} was updated successfully!"; // VeiwData는 redirect하면 데이터가 사라짐
                return RedirectToAction("Register");

            }
            return View(instModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MusicInstrument instToDelete = await _context.MusicInstruments.FindAsync(id);

            if (instToDelete == null) 
            { 
                return NotFound();
            }
            return View(instToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            MusicInstrument instToDelete = await _context.MusicInstruments.FindAsync(id);

            if (instToDelete != null)
            {
                _context.MusicInstruments.Remove(instToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{instToDelete.Title} was deleted successfully!";
                return RedirectToAction("Register");
            }

            TempData["Message"] = "This game was already deleted";
            return RedirectToAction("index");

        }

        public async Task<IActionResult> Details(int id)
        {
            MusicInstrument instToDetail = await _context.MusicInstruments.FindAsync(id);
            if (instToDetail == null)
            {
                return NotFound();
            }
            return View(instToDetail);
        }

    }
}
