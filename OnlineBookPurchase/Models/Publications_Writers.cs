using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Models
{
    public class Publications_Writers
    {
        public int PublicationsId { get; set; }
        public Publications Publications { get; set; }

        public int WriterId { get; set; }
        public Writer Writer { get; set; }

    }
}
