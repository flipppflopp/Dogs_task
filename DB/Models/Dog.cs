using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    [DogValidation.ValidDogAttribute]
    public class Dog
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string name { get; set; }
        
        [Required]
        public string color { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Tail length should be greater than 0.")]
        public double tail_length { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Weight should be greater than 0.")]
        public double weight { get; set; }
    }
}