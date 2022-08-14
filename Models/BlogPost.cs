using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyChange.Models
{
    public class BlogPost
    {
        [Key]
        public int BlogId { get; set; }
        
        [Required(ErrorMessage = ("Blog Title Required"))]
        [DisplayName("Blog Title")]
        public string BlogTitle { get; set;}

        [Required(ErrorMessage = ("Blog Content Required"))]
        [DisplayName("Blog Content")]
        public string BlogContent { get; set;}

        [Required(ErrorMessage = ("Blog Image Url Required"))]
        [DisplayName("Blog Image Url")]
        public string BlogImageUrl { get; set;}




        //Navigation properties
        public string Id { get; set; }
        public virtual User User { get; set; }
    }
}
