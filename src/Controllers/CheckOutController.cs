using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using ToyChange.Data;
using ToyChange.Models;

namespace ToyChange.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckOutController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult Create(string stripeToken, long id)
        //{
        //    List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
        //    CartItem cartItem = cart.FirstOrDefault(c => c.ProductId == id);

        //    var chargeOptions = new ChargeCreateOptions() {

        //        Amount = ((long)cartItem.Price*100),
        //        Currency = "usd",
        //        Source =stripeToken,
        //        Metadata = new Dictionary<string, string>() {
        //            {"ProductId", cartItem.ProductId.ToString()},
        //            {"ProductName", cartItem.ProductName}

        //        }
        //    };

        //    var service = new ChargeService();
        //    Charge charge = service.Create(chargeOptions);

        //    if(charge.Status == "succeeded")
        //    {
        //        return View("Success");
        //    }
        //    else
        //    {
        //        return View("Failure");
        //    }

        //}

    }
}
