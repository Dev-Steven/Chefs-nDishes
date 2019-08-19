using System.ComponentModel.DataAnnotations;
using System;
namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required]
        [Display(Name="Dish Name")]
        public string Name {get;set;}

        [Required]
        public int? Calories {get;set;}

        [Required]
        public string Description {get;set;}

        [Required]
        public int? ChefId {get;set;}

        public Chef Creator {get;set;}

        [Required]
        public int Tastiness {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}