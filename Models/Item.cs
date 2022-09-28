﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name =("Price $"))]
        public float Price { get; set; }

        [Required(ErrorMessage = ("Image Url required"))]
        [Display(Name = ("Image URL"))]

        public string ImageUrl { get; set; }

        public ItemCategory ItemCategory { get; set; }


        //Navigation properties
        //[ForeignKey("Id")]
        public string Id { get; set; }
        public virtual User User { get; set; }
        
        public int OrderId { get; set; }
        //[ForeignKey("OrderId")]
        public virtual Order Order { get; set; }




    }
}