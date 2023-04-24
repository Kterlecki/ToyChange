namespace ToyChange.Models
{
    public class ShoppingCart
    {
        private List<Item> items;

        public ShoppingCart()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            var i = items.FirstOrDefault(p => p.ItemId.Equals(item.ItemId));
            if (i != null)
            {
                var item_cart = new Item() {
                    ItemId = item.ItemId,
                    Title = item.Title,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                    ItemCategory = item.ItemCategory,
                    Order = item.Order
                };
                items.Add(item_cart);
            }
        }
    }
}
