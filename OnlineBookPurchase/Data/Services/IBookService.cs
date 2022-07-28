using OnlineBookPurchase.Data.Base;
using OnlineBookPurchase.Data.ViewModels;
using OnlineBookPurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Data.Services
{
    public interface IBookService : IEntityBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<NewBookDropdownsVM> GetNewBookDropdownsValues();

        Task AddNewBookAsync(NewBookVM data);

        Task UpdateBookAsync(NewBookVM data);

    }
}
