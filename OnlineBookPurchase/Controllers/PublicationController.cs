using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class PublicationController : Controller
    {
        private readonly IPublicationService _service;

        public PublicationController(IPublicationService service)
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

        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Publications publications)
        {
            if (!ModelState.IsValid)
            {
                return View(publications);
            }
            await _service.AddAsync(publications);
            return RedirectToAction(nameof(Index));
        }

        //Get: Publication/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var publicationDetails = await _service.GetByIdAsync(id);

            if (publicationDetails == null) return View("NotFound");
            return View(publicationDetails);
        }


        //Get: Publication/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var publicationDetails = await _service.GetByIdAsync(id);
            if (publicationDetails == null) return View("NotFound");
            return View(publicationDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var publicationDetails = await _service.GetByIdAsync(id);
            if (publicationDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        //Get: Publication/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var publicationDetails = await _service.GetByIdAsync(id);
            if (publicationDetails == null) return View("NotFound");
            return View(publicationDetails);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Publications publications)
        {
            if (!ModelState.IsValid)
            {
                return View(publications);
            }
            await _service.UpdateAsync(id, publications);
            return RedirectToAction(nameof(Index));
        }
    }
}
