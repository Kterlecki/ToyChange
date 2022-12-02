using System.ComponentModel.DataAnnotations;
using ToyChange.Data.Enums;

namespace ToyChange.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = ("Title required"))]
        public string Title { get; set; }

        [Required(ErrorMessage = ("Description required"))]
        public string Description { get; set; }

        [Required(ErrorMessage = ("Price required"))]
        [Display(Name = ("Price $"))]
        public float Price { get; set; }

        [Required(ErrorMessage = ("Image Url required"))]
        [Display(Name = ("Image URL"))]

        public string ImageUrl { get; set; }

        public ItemCategory ItemCategory { get; set; }


        //Navigation properties


        public virtual Order Order { get; set; }




    }
}
