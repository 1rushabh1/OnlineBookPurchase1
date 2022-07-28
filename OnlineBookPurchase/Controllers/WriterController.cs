using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class WriterController : Controller
    {
        private readonly IWriterService _service;

        public WriterController(IWriterService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Writers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Writer writer)
        {
            if(!ModelState.IsValid)
            {
                return View(writer);
            }
            await _service.AddAsync(writer);
            return RedirectToAction(nameof(Index));
        }

        //Get: Writers/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var writerDetails = await _service.GetByIdAsync(id);

            if (writerDetails == null) return View("NotFound");
            return View(writerDetails);
        }


        //Get: Writers/Edit/1
        public async Task<IActionResult> Delete(int id)
        {
            var writerDetails = await _service.GetByIdAsync(id);
            if (writerDetails == null) return View("NotFound");
            return View(writerDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var writerDetails = await _service.GetByIdAsync(id);
            if (writerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        //Get: Writers/Delete/1
        public async Task<IActionResult> Edit(int id)
        {
            var writerDetails = await _service.GetByIdAsync(id);
            if (writerDetails == null) return View("NotFound");
            return View(writerDetails);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Writer writer)
        {
            if (!ModelState.IsValid)
            {
                return View(writer);
            }
            await _service.UpdateAsync(id, writer);
            return RedirectToAction(nameof(Index));
        }

    }
}
