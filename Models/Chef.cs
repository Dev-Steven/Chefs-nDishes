using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;   // For List types

    namespace ChefsNDishes.Models
    {
        public class Chef
        {
            [Key]
            public int ChefId {get;set;}

            [Required]
            public string FirstName {get;set;}

            [Required]
            public string LastName {get;set;}

            [Required]
            public string Birthday {get;set;}

            public List<Dish> CreatedDishes {get;set;}

            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;

        }

        public class BirthdayValidator : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime birthday = Convert.ToDateTime(value);
                DateTime today = DateTime.Now;

                if(birthday>today)
                {
                    return new ValidationResult("Not a valid birthday");
                }
                return ValidationResult.Success;

            }
        }

    }