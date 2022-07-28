using OnlineBookPurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Data.ViewModels
{
    public class NewBookDropdownsVM
    {
        public NewBookDropdownsVM()
        {
            Writers = new List<Writer>();
            Publications = new List<Publications>();
        }
        public List<Writer> Writers { get; set; }
        public List<Publications> Publications { get; set; }

    }
}
