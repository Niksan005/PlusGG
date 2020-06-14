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
    public class MainRunesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainRunesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RuneCategoriesController
        public ActionResult Index()
        {
            IEnumerable<MainRunesViewModel> model = _context.MainRunes.Include(x => x.RuneCategory).Select(x => new MainRunesViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageSrc = x.Logo,
                Sort = x.Sort,
                RuneCategoryName = x.RuneCategory.Name
            }).ToList();
            return View(model);
        }

        // GET: RuneCategoriesController/Create
        public ActionResult AddEdit(int id)
        {
            var model = new MainRunesViewModel();
            var rune = _context.MainRunes.Where(x => x.Id == id)
                .Include(x => x.RuneCategory)
                .FirstOrDefault();
            if (rune != null)
            {
                model.Id = id;
                model.Name = rune.Name;
                model.Description = rune.Description;
                model.ImageSrc = rune.Logo;
                model.Sort = rune.Sort;
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
        public ActionResult AddEdit(MainRunesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mainrune = _context.MainRunes.Find(model.Id);
                if (mainrune == null)
                {
                    mainrune = new Data.Models.MainRunes();
                    _context.MainRunes.Add(mainrune);
                }
                mainrune.Name = model.Name;
                mainrune.Description = model.Description;
                mainrune.Sort = model.Sort;
                mainrune.RuneCategory = _context.RuneCategories.FirstOrDefault(x => x.Id == model.RuneCategoryId);
                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        var bytearray = ms.ToArray();
                        mainrune.Logo = string.Format("data:{1};base64,{0}",
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
            var rune = _context.MainRunes.Find(id);
            if (rune == null)
            {
                return NotFound();
            }
            _context.MainRunes.Remove(rune);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
