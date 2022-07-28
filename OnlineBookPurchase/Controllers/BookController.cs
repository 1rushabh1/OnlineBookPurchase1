using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBookPurchase.Data;
using OnlineBookPurchase.Data.Services;
using OnlineBookPurchase.Data.Static;
using OnlineBookPurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class BookController : Controller
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        public async Task<IActionResult> BookIndex()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Publications);
            return View(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await _service.GetAllAsync(n => n.Publications);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allBooks.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString,
                    StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
           
            }

            return View("Index",allBooks);
        }

        //Get: Books/Create
        public async Task<IActionResult> Create()
        {
            var bookDropdownsData = await _service.GetNewBookDropdownsValues();

            ViewBag.WriterId = new SelectList(bookDropdownsData.Writers, "Id", "FullName");
            ViewBag.PublicationId = new SelectList(bookDropdownsData.Publications, "Id", "FullName");

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.WriterId = new SelectList(bookDropdownsData.Writers, "Id", "FullName");
                ViewBag.PublicationId = new SelectList(bookDropdownsData.Publications, "Id", "FullName");

                return View(book);
            }
            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        //Get: Books/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);

            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }


        //Get: Books/Edit/1
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        //Get: Books/Delete/1
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Name = bookDetails.Name,
                Description = bookDetails.Description,
                Price = bookDetails.Price,
                ImageURL = bookDetails.ImageURL,
                BookCategory = bookDetails.BookCategory,
                PublicationId = bookDetails.PublicationId,
                WriterIds = bookDetails.Book_Writer.Select(n => n.WriterId).ToList(),
            };

            var bookDropdownsData = await _service.GetNewBookDropdownsValues();

            ViewBag.WriterId = new SelectList(bookDropdownsData.Writers, "Id", "FullName");
            ViewBag.PublicationId = new SelectList(bookDropdownsData.Publications, "Id", "FullName");

            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, NewBookVM book)
        {
            if (id != book.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.WriterId = new SelectList(bookDropdownsData.Writers, "Id", "FullName");
                ViewBag.PublicationId = new SelectList(bookDropdownsData.Publications, "Id", "FullName");

                return View(book);
            }
            await _service.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
