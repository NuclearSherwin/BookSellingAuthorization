using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookselling.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")] public Category Category { get; set; }
    }
}