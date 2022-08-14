using Microsoft.AspNetCore.Identity;

namespace ToyChange.Models
{
    public class User : IdentityUser
    {





        //Navigation properties
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int BlogId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}
