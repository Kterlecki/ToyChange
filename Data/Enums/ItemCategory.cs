using System.ComponentModel.DataAnnotations;

namespace ToyChange.Data.Enums
{
    public enum ItemCategory
    {
        Technic,
        [Display(Name = "Jurassic World")]
        Jurassic_World, 
        City,
        Ninjago
    }
}
