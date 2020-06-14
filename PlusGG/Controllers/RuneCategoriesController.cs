using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlusGG.Data;
using PlusGG.Data.Migrations;
using PlusGG.Models;

namespace PlusGG.Controllers
{
    /// <summary>
    /// http://www.binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
    /// Image Helper
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RuneCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RuneCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RuneCategoriesController
        public ActionResult Index()
        {
            IEnumerable<RuneCategoriesViewModel> model = _context.RuneCategories.Select(x => new RuneCategoriesViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageSrc = x.Logo
            }).ToList();
            return View(model);
        }

        // GET: RuneCategoriesController/Create
        public ActionResult AddEdit(int id)
        {
            var model = new RuneCategoriesViewModel();
            var rune = _context.RuneCategories.Find(id);
            if (rune != null)
            {
                model.Id = id;
                model.Name = rune.Name;
                model.Description = rune.Description;
                model.ImageSrc = rune.Logo;
            }
            return View(model);
        }

        // POST: RuneCategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(RuneCategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rune = _context.RuneCategories.Find(model.Id);
                if (rune == null)
                {
                    rune = new Data.Models.RuneCategories();
                    _context.RuneCategories.Add(rune);
                }
                rune.Name = model.Name;
                rune.Description = model.Description;
                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        var bytearray = ms.ToArray();
                        rune.Logo = string.Format("data:{1};base64,{0}",
                            Convert.ToBase64String(bytearray),
                            model.Image.ContentType);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        // GET: RuneCategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            var rune = _context.RuneCategories.Find(id);
            if (rune == null)
            {
                return NotFound();
            }
            _context.RuneCategories.Remove(rune);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
