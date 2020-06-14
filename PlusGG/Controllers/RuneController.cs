using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class RuneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RuneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RuneCategoriesController
        public ActionResult Index()
        {
            IEnumerable<RuneViewModel> model = _context.Runes
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.RuneCategory.Id)
                .ThenBy(x => x.LevelRune)
                .ThenBy(x => x.Sort)
                .Select(x => new RuneViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageSrc = x.Logo,
                    Sort = x.Sort,
                    Level = x.LevelRune,
                    RuneCategoryName = x.RuneCategory.Name
                }).ToList();
            return View(model);
        }

        // GET: RuneCategoriesController/Create
        public ActionResult AddEdit(int id)
        {
            var model = new RuneViewModel();
            var rune = _context.Runes.Where(x => x.Id == id)
                .Include(x => x.RuneCategory)
                .FirstOrDefault();
            if (rune != null)
            {
                model.Id = id;
                model.Name = rune.Name;
                model.Description = rune.Description;
                model.ImageSrc = rune.Logo;
                model.Sort = rune.Sort;
                model.Level = rune.LevelRune;
                model.RuneCategoryId = rune.RuneCategory.Id;
            }
            model.AllRuneCategories = _context.RuneCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }

        // POST: RuneCategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(RuneViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rune = _context.Runes.Find(model.Id);
                if (rune == null)
                {
                    rune = new Data.Models.Runes();
                    _context.Runes.Add(rune);
                }
                rune.Name = model.Name;
                rune.Description = model.Description;
                rune.Sort = model.Sort;
                rune.LevelRune = model.Level;
                rune.RuneCategory = _context.RuneCategories.FirstOrDefault(x => x.Id == model.RuneCategoryId);
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
            var rune = _context.Runes.Find(id);
            if (rune == null)
            {
                return NotFound();
            }
            _context.Runes.Remove(rune);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
