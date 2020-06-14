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
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemsController
        public ActionResult Index()
        {
            IEnumerable<ItemViewModel> model = _context.Items.Select(x => new ItemViewModel {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageSrc = x.Logo
            }).ToList();
            return View(model);
        }

        // GET: ItemsController/Create
        public ActionResult AddEdit(int id)
        {
            var model = new ItemViewModel();
            var item = _context.Items.Find(id);
            if (item != null)
            {
                model.Id = id;
                model.Name = item.Name;
                model.Description = item.Description;
                model.ImageSrc = item.Logo;
            }
            return View(model);
        }

        // POST: ItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Items.Find(model.Id);
                if (item == null)
                {
                    item = new Data.Models.Item();
                    _context.Items.Add(item);
                }
                item.Name = model.Name;
                item.Description = model.Description;
                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        var bytearray = ms.ToArray();
                        item.Logo = string.Format("data:{1};base64,{0}",
                            Convert.ToBase64String(bytearray),
                            model.Image.ContentType);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        // GET: ItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Items.Remove(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
