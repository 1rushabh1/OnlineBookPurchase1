using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Models
{
    public class Book_Writer
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int WriterId { get; set; }
        public Writer Writer { get; set; }
    }
}
