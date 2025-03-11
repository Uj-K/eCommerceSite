using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly MusicInstrumentContext _context;
        private const string Cart = "ShoppingCart";

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

            /*
             * 객체 간 매핑(Object-to-Object Mapping)은 한 객체의 데이터를 다른 객체로 변환하는 과정
             * 주로 데이터 전송 객체(Data Transfer Object, DTO)와 도메인 모델 간의 변환, 또는 다른 계층 간의 데이터 전달을 위해 사용됨
             * 객체 매핑을 수동으로 수행할 수 있으며, 이를 위해 각 속성을 하나씩 할당하는 방법을 사용할 수 있음
             * 예를 들어 밑의 코드도, MusicInstrument 객체를 CartProductViewModel 객체로 매핑하는 것
             * 
             * 또한 이 코드는 객체 초기화 구문(Object Initialization Syntax)도 이기도 하다.
             * 객체를 생성하고 초기화하는 간결한 방법을 제공합니다. 
             * 객체 초기화 구문을 사용하면 생성자 호출 후에 속성을 설정하는 대신, 
             * 객체를 생성할 때 속성을 한 번에 설정할 수 있습니다.
             */
            CartProductViewModel cartProduct = new()
            {
                InstrumentID = prodToAdd.InstrumentID,
                Title = prodToAdd.Title,
                Price = prodToAdd.Price
            };

            List<CartProductViewModel> cartProducts = GetExistingCartData();
            cartProducts.Add(cartProduct);
            /* 시간상 이번 수업에서 다루지는 못하지만 
             * 같은 제품이 그냥 따로따로 카드에 담기는것을 방지하기 위해
             * add 하기전에 이미 카드에 있는지 확인하는것도 좋은 방법이다.
             */

            WriteShoppingCartCookie(cartProducts);

            /* Append 은 overload 인데 (string key, string value)과 
             * (string key, string value, CookieOptions options) 처럼
             * 동일한 메서드 이름을 사용하지만, 매개변수의 수나 타입이 다를 수 있음을 의미.*/

            // Todo: Add item to cart cookie
            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "MusicInstruments");
        }

        private void WriteShoppingCartCookie(List<CartProductViewModel> cartProducts)
        {
            string cookieData = JsonConvert.SerializeObject(cartProducts);

            /* 직렬화(Serialization)는 객체의 상태를 저장하거나 전송할 수 있는 형식으로 변환하는 과정
             * 직렬화된 데이터는 파일, 데이터베이스, 메모리, 네트워크 등을 통해 저장되거나 전송될 수 있음
             * 반대로, 역직렬화(Deserialization)는 직렬화된 데이터를 다시 원래의 객체로 복원하는 과정
             * C#에서 가장 일반적으로 사용되는 직렬화 형식은 JSON 이다. 
             */
            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
                // 1년후에 쿠키 익스파이얼
            });
        }

        /// <summary>
        /// Return the current list of products in the users shopping card cookie
        /// If there is no cookies, an empty list will be returned
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<CartProductViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];

            /* string.IsNullOrEmpty:
             * 문자열이 null이거나 빈 문자열("")인지 확인
             * 공백 문자(예: 스페이스, 탭 등)는 빈 문자열로 간주하지 않음
             * 
             * string.IsNullOrWhiteSpace:
             * 문자열이 null, 빈 문자열(""), 
             * 또는 공백 문자(스페이스, 탭, 줄 바꿈 등)로만 구성되어 있는지 확인
             */
            if (string.IsNullOrWhiteSpace(cookie)) 
            { 
                return new List<CartProductViewModel>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<CartProductViewModel>>(cookie);
                // JSON 문자열을 CartProductViewModel 객체로 Deserialize
            }
        }

        /// <summary>
        /// Read Shopping Cart data and convert the list of view model
        /// </summary>
        /// <returns>cartProducts</returns>
        public IActionResult Summary()
        {
            List<CartProductViewModel> cartProducts = GetExistingCartData();
            return View(cartProducts);
        }

        /// <summary>
        /// Get the shopping cart list 
        /// then remove selected product from the list 
        /// and re-save all back to list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Remove(int id) 
        {
            List<CartProductViewModel> cartProducts = GetExistingCartData();

            CartProductViewModel? targetProduct = cartProducts.FirstOrDefault(p => p.InstrumentID == id);

            cartProducts.Remove(targetProduct);

            WriteShoppingCartCookie(cartProducts);

            return RedirectToAction(nameof(Summary));

        }
    }
}
