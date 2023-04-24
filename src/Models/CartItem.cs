using System.ComponentModel.DataAnnotations;

namespace ToyChange.Models
{
    public class CartItem
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
            {
            get { return Quantity * Price; }
            }
        public string ImageUrl { get; set; }

        public CartItem()
        {
        }
        public CartItem(Item item)
        {
            ProductId = item.ItemId;
            ProductName = item.Title;
            Price = (decimal)item.Price;
            Quantity = 1;
            ImageUrl = item.ImageUrl;
        }
    }
}
