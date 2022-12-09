using Microsoft.AspNetCore.Mvc;
using Stripe;
using ToyChange.Data;
using ToyChange.Models;
using ToyChange.Models.ViewModel;

namespace ToyChange.Controllers
{
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new() {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(cartVM);
        }
        public async Task<IActionResult> Add(int id)
        {
            Item item = await _context.Item.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if (cartItem == null)
            {
                cart.Add(new CartItem(item));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            TempData["Success"] = "The product has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        

        public async Task<IActionResult> Remove(int id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            cart.RemoveAll(x => x.ProductId == id);



            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been Removed!";

            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(string stripeToken, [FromRoute]long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.FirstOrDefault(c => c.ProductId == id);

            var chargeOptions = new ChargeCreateOptions() {

                Amount = ((long)cartItem.Price * 100),
                Currency = "usd",
                Source = stripeToken,
                Metadata = new Dictionary<string, string>() {
                    {"ProductId", cartItem.ProductId.ToString()},
                    {"ProductName", cartItem.ProductName}

                }
            };

            
            var service = new ChargeService();
            Charge charge = service.Create(chargeOptions);

            if (charge.Status == "succeeded")
            {
                HttpContext.Session.Remove("Cart");
                return View("Success");
            }
            else
            {
                return View("Failure");
            }

            //Order order = new Order();
            //OrdersController ordersController = new OrdersController( order);


        }
    }
}
