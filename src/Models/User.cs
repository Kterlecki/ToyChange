using Microsoft.AspNetCore.Identity;

namespace ToyChange.Models
{
    public class User : IdentityUser
    {





        //Navigation properties




        public virtual ICollection<Order> Order { get; set; }


        public virtual ICollection<BlogPost> BlogPost { get; set; }
    }
}
