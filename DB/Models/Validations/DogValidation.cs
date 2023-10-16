using System;
using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class DogValidation
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class ValidDogAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is Dog dog)
                {
                    if (string.IsNullOrEmpty(dog.name) || string.IsNullOrEmpty(dog.color) ||
                        dog.tail_length <= 0 || dog.weight <= 0)
                    {
                        return new ValidationResult("Invalid dog object.");
                    }
                }

                return ValidationResult.Success;
            }
        }

    }
}