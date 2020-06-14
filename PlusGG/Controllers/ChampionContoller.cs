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
using PlusGG.Data.Models;
using PlusGG.Models;

namespace PlusGG.Controllers
{
    /// <summary>
    /// http://www.binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
    /// Image Helper
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ChampionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChampionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemsController
        public ActionResult Index()
        {
            IEnumerable<ChampionViewModel> model = _context.Champions.Select(x => new ChampionViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImageSrc = x.Logo
            }).ToList();
            return View(model);
        }

        // GET: ItemsController/Create
        public ActionResult AddEdit(int id)
        {
            var model = new ChampionViewModel { 
                SpellP = new SpellViewModel(),
                SpellQ = new SpellViewModel(),
                SpellW = new SpellViewModel(),
                SpellE = new SpellViewModel(),
                SpellR = new SpellViewModel()
            };
            var champion = _context.Champions.Where(x => x.Id == id)
                .Include(x => x.SummonerSpellD)
                .Include(x => x.SummonerSpellF)
                .Include(x => x.Spells)
                .FirstOrDefault();
            if (champion != null)
            {
                model.Id = id;
                model.Name = champion.Name;
                model.SummonerSpellDId = champion.SummonerSpellD?.Id ?? 0;
                model.SummonerSpellFId = champion.SummonerSpellF?.Id ?? 0;
                model.SpellP = champion.Spells.Where(x => x.SpellType == "P").Select(x => new SpellViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageSrc = x.Logo
                }).FirstOrDefault() ?? new SpellViewModel();
                model.SpellQ = champion.Spells.Where(x => x.SpellType == "Q").Select(x => new SpellViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageSrc = x.Logo
                }).FirstOrDefault() ?? new SpellViewModel();
                model.SpellW = champion.Spells.Where(x => x.SpellType == "W").Select(x => new SpellViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageSrc = x.Logo
                }).FirstOrDefault() ?? new SpellViewModel();
                model.SpellE = champion.Spells.Where(x => x.SpellType == "E").Select(x => new SpellViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageSrc = x.Logo
                }).FirstOrDefault() ?? new SpellViewModel();
                model.SpellR = champion.Spells.Where(x => x.SpellType == "R").Select(x => new SpellViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageSrc = x.Logo
                }).FirstOrDefault() ?? new SpellViewModel();
                model.ImageSrc = champion.Logo;
            }
            model.AllSummonerSpells = _context.SummonerSpells.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return View(model);
        }

        // POST: ItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(ChampionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var champion = _context.Champions.Where(x => x.Id == model.Id)
                    .Include(x => x.Spells)
                    .FirstOrDefault();
                if (champion == null)
                {
                    champion = new Champion
                    {
                        Spells = new List<Spell>()
                    };
                    _context.Champions.Add(champion);
                }
                champion.Name = model.Name;
                champion.SummonerSpellD = _context.SummonerSpells.FirstOrDefault(x => x.Id == model.SummonerSpellDId);
                champion.SummonerSpellF = _context.SummonerSpells.FirstOrDefault(x => x.Id == model.SummonerSpellFId);


                SpellInjection(model.SpellP, champion, "P");
                SpellInjection(model.SpellQ, champion, "Q");
                SpellInjection(model.SpellW, champion, "W");
                SpellInjection(model.SpellE, champion, "E");
                SpellInjection(model.SpellR, champion, "R");

                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        var bytearray = ms.ToArray();
                        champion.Logo = string.Format("data:{1};base64,{0}",
                            Convert.ToBase64String(bytearray),
                            model.Image.ContentType);
                    }
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            model.AllSummonerSpells = _context.SummonerSpells.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return View(model);
        }

        private static void SpellInjection(SpellViewModel spellVM, Champion champion, string spellType)
        {
            var spellDb = champion.Spells.FirstOrDefault(x => x.Id == spellVM.Id && x.SpellType == spellType);
            if (spellDb == null)
            {
                spellDb = new Spell();
                spellDb.SpellType = spellType;
                champion.Spells.Add(spellDb);
            }
            spellDb.Name = spellVM.Name;
            spellDb.Description = spellVM.Description;

            if (spellVM.Image != null && spellVM.Image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    spellVM.Image.CopyTo(ms);
                    var bytearray = ms.ToArray();
                    spellDb.Logo = string.Format("data:{1};base64,{0}",
                        Convert.ToBase64String(bytearray),
                        spellVM.Image.ContentType);
                }
            }
        }

        // GET: ItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            var champion = _context.Champions.Where(x => x.Id == id)
                .Include(x => x.SummonerSpellD)
                .Include(x => x.SummonerSpellF)
                .Include(x => x.Spells)
                .Include(x => x.MatchUpsMain)
                .Include(x => x.MatchUpsVs)
                .FirstOrDefault();
            if (champion == null)
            {
                return NotFound();
            }

            _context.Spells.RemoveRange(champion.Spells);
            _context.MatchUps.RemoveRange(champion.MatchUpsMain);
            _context.MatchUps.RemoveRange(champion.MatchUpsVs);

            _context.Champions.Remove(champion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
