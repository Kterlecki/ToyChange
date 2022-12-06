//using ToyChange.Data;
//using ToyChange.Models;


//namespace ToyChange.Logic
//{
//    public class ShoppingCartActions : IDisposable
//    {
//        public string ShoppingCartId { get; set; }

//        private  ApplicationDbContext _db;

//        public ShoppingCartActions(ApplicationDbContext context)
//        {
//            _db = context;
//        }

//        public const string CartSessionKey = "CartId";

//        public void AddToCart(int id)
//        {
//            // Retrieve the product from the database.           
//            ShoppingCartId = GetCartId();

//            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
//                c => c.CartId == ShoppingCartId
//                && c.ItemId == id);
//            if (cartItem == null)
//            {
//                // Create a new cart item if no cart item exists.                 
//                cartItem = new CartItem {
//                    CartItemId = Guid.NewGuid().ToString(),
//                    ItemId = id,
//                    CartId = ShoppingCartId,
//                    Item = _db.Item.SingleOrDefault(
//                   p => p.ItemId == id),
//                    Quantity = 1,
//                    DateCreated = DateTime.Now
//                };

//                _db.ShoppingCartItems.Add(cartItem);
//            }
//            else
//            {
//                // If the item does exist in the cart,                  
//                // then add one to the quantity.                 
//                cartItem.Quantity++;
//            }
//            _db.SaveChanges();
//        }

//        public void Dispose()
//        {
//            if (_db != null)
//            {
//                _db.Dispose();
//                _db = null;
//            }
//        }

//        public string GetCartId()
//        {
//            //HttpContext.
//            //if (HttpContext.Current.Session[CartSessionKey] == null)
//            //{
//            //    if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
//            //    {
//            //        HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
//            //    }
//            //    else
//            //    {
//            //        // Generate a new random GUID using System.Guid class.     
//            //        Guid tempCartId = Guid.NewGuid();
//            //        HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
//            //    }
//            //}
//            //return HttpContext.Current.Session[CartSessionKey].ToString();
//            return null;
//        }

//        public List<CartItem> GetCartItems()
//        {
//            ShoppingCartId = GetCartId();

//            return _db.ShoppingCartItems.Where(
//                c => c.CartId == ShoppingCartId).ToList();
//        }


//    }
//}
