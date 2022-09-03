using System.Collections.Generic;
using bookselling.Models;

namespace bookselling.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<Cart> ListCarts { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}