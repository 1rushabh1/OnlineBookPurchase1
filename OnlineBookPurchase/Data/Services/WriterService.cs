using Microsoft.EntityFrameworkCore;
using OnlineBookPurchase.Data.Base;
using OnlineBookPurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Data.Services
{
    public class WriterService : EntityBaseRepository<Writer>, IWriterService
    {
        public WriterService(AppDbContext context) : base(context) { 
        
        }
             
    }
}
