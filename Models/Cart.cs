using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookselling.Models
{
    public class Cart
    {
        [Key] public int Id { get; set; }
        [Required] public string CustomerId { get; set; }
        [Required] public int BookId { get; set; }

        [ForeignKey("CustomerId")] private ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("BookId")] public Book Book { get; set; }
    }
}