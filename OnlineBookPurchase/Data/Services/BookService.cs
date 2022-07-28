using Microsoft.EntityFrameworkCore;
using OnlineBookPurchase.Data.Base;
using OnlineBookPurchase.Data.ViewModels;
using OnlineBookPurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Data.Services
{
    public class BookService : EntityBaseRepository<Book>, IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context) : base(context) {
            _context = context;
        }

        public async Task AddNewBookAsync(NewBookVM data)
        {
            var newBook = new Book()
            {
                Name = data.Name,
                Description = data.Description,
                ImageURL = data.ImageURL,
                BookCategory = data.BookCategory,
                PublicationId = data.PublicationId,
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            //Add Book Writers
            foreach(var writerId in data.WriterIds)
            {
                var newWriterMovie = new Book_Writer()
                {
                    BookId = newBook.Id,
                    WriterId = writerId
                };
                await _context.Book_Writers.AddAsync(newWriterMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails = await _context.Books
                .Include(p => p.Publications)
                .Include(wr => wr.Book_Writer).ThenInclude(w => w.Writer)
                .FirstOrDefaultAsync(n => n.Id == id);

            return bookDetails;
        }

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            var response = new NewBookDropdownsVM();
            response.Writers = await _context.Writers.OrderBy(n => n.FullName).ToListAsync();
            response.Publications = await _context.Publications.OrderBy(n => n.FullName).ToListAsync();

            return response;
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {
            var dbBook = await _context.Books.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbBook != null)
            {

                dbBook.Name = data.Name;
                dbBook.Description = data.Description;
                dbBook.Price = data.Price;
                dbBook.ImageURL = data.ImageURL;
                dbBook.BookCategory = data.BookCategory;
                dbBook.PublicationId = data.PublicationId;

                await _context.SaveChangesAsync();
            }
                //Remove existing writers
                var existingWritesDb = _context.Book_Writers.Where(n => n.BookId == data.Id).ToList();
                _context.Book_Writers.RemoveRange(existingWritesDb);
                await _context.SaveChangesAsync();
                           

            //Add Book Writers
            foreach (var writerId in data.WriterIds)
            {
                var newWriterMovie = new Book_Writer()
                {
                    BookId = data.Id,
                    WriterId = writerId
                };
                await _context.Book_Writers.AddAsync(newWriterMovie);
            }
            await _context.SaveChangesAsync();

        }
    }
}
