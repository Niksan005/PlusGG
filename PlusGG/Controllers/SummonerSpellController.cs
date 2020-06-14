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
    public class SummonerSpellController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SummonerSpellController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SummonerSpellController
        public ActionResult Index()
        {
            IEnumerable<SummonerSpellViewModel> model = _context.SummonerSpells.Select(x => new SummonerSpellViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageSrc = x.Logo
            }).ToList();
            return View(model);
        }

        // GET: SummonerSpellController/Create
        public ActionResult AddEdit(int id)
        {
            var model = new SummonerSpellViewModel();
            var summonerSpell = _context.SummonerSpells.Find(id);
            if (summonerSpell != null)
            {
                model.Id = id;
                model.Name = summonerSpell.Name;
                model.Description = summonerSpell.Description;
                model.ImageSrc = summonerSpell.Logo;
            }
            return View(model);
        }

        // POST: SummonerSpellController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(SummonerSpellViewModel model)
        {
            if (ModelState.IsValid)
            {
                var summonerSpell = _context.SummonerSpells.Find(model.Id);
                if (summonerSpell == null)
                {
                    summonerSpell = new Data.Models.SummonerSpell();
                    _context.SummonerSpells.Add(summonerSpell);
                }
                summonerSpell.Name = model.Name;
                summonerSpell.Description = model.Description;
                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        var bytearray = ms.ToArray();
                        summonerSpell.Logo = string.Format("data:{1};base64,{0}",
                            Convert.ToBase64String(bytearray),
                            model.Image.ContentType);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        // GET: SummonerSpellController/Delete/5
        public ActionResult Delete(int id)
        {
            var summonerSpell = _context.SummonerSpells.Find(id);
            if (summonerSpell == null)
            {
                return NotFound();
            }
            _context.SummonerSpells.Remove(summonerSpell);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
