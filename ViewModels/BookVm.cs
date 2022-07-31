using System.Collections.Generic;
using bookselling.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookselling.ViewModels
{
    public class BookVm
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}