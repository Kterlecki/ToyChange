using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyChange.Models
{
    public class User : IdentityUser
    {





        //Navigation properties
        
        public virtual ICollection<Item> Item { get; set; }

        
        public virtual ICollection<Order> Order { get; set; }
        
        
        public virtual ICollection<BlogPost> BlogPost { get; set; }
    }
}
