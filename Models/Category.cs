using System.ComponentModel.DataAnnotations;

namespace bookselling.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
    }
}