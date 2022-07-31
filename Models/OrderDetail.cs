using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookselling.Models
{
    public class OrderDetail
    {
        [Key] public int Id { get; set; }
        [Required] public int OrderHeaderId { get; set; }
        [Required] public int BookId { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public double Price { get; set; }

        // foreign keys
        [ForeignKey("OrderHeaderId")] public OrderHeader OrderHeader { get; set; }
        [ForeignKey("BookId")] public Book Book { get; set; }
    }
}