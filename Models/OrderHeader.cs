using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookselling.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required] public double Total { get; set; }
        [Required] public DateTime OrderDate { get; set; }
        
        // foreign key
        [ForeignKey("CustomerId")] public ApplicationUser ApplicationUser { get; set; }
    }
}