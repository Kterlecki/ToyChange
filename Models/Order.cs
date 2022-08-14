using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyChange.Models
{
    public class Order
    {
        [Key]
        [Required(ErrorMessage =("OrderId required"))]
        [DisplayName("Order ID")]
        public int OrderId { get; set; }

        [Required(ErrorMessage =("Date and time must be filled in"))]
        [DisplayName("Order Date")]  
        public DateTime OrderDate { get; set; }






        //Navigation properties
        public string Id { get; set; }
        public virtual User User { get; set; }


        public int ItemId { get; set; } 
        public virtual Item Item { get; set; }


    }
}
