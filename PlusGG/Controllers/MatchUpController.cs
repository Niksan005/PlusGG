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
    public class MatchUpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchUpController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemsController
        public ActionResult Index(int cid)
        {
            IEnumerable<MatchUpViewModel> model = _context.MatchUps
                .Include(x => x.VsChampion)
                .Where(x => x.MainChampion.Id == cid)
                .Select(x => new MatchUpViewModel
                {
                    Id = x.Id,
                    WinRate = x.WinRate,
                    StrongerEarly = x.StrongerEarly,
                    StrongerMid = x.StrongerMid,
                    StrongerLate = x.StrongerLate,
                    VsChampion = new ChampionViewModel {
                        ImageSrc = x.VsChampion.Logo
                    }
                }).ToList();
            ViewData["cid"] = cid;
            return View(model);
        }

        // GET: ItemsController/Create
        public ActionResult AddEdit(int cid, int id)
        {
            var model = new MatchUpViewModel
            {
                Items = new List<ItemViewModel>()
            };
            var item = _context.MatchUps.Where(c => c.Id == id)
                .Include(x => x.VsChampion)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryMainRune)
                        .ThenInclude(y => y.RuneCategory)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryRune1)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryRune2)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryRune3)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.SecondaryRune1)
                        .ThenInclude(y => y.RuneCategory)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.SecondaryRune2)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.SecondaryRune3)
                .Include(x => x.ItemSets)
                    .ThenInclude(x => x.Item)
                .FirstOrDefault();
            if (item != null)
            {
                model.Id = id;
                model.ItemIDs = string.Join(",", item.ItemSets.Select(x => x.Item.Id));
                model.VsChampionId = item.VsChampion?.Id;
                model.WinRate = item.WinRate;
                model.StrongerEarly = item.StrongerEarly;
                model.StrongerMid = item.StrongerMid;
                model.StrongerLate = item.StrongerLate;
                model.Items = item.ItemSets
                    .OrderBy(x => x.Sort)
                    .Select(x => new ItemViewModel
                    {
                        Id = x.Item.Id,
                        Name = x.Item.Name,
                        Description = x.Item.Description,
                        ImageSrc = x.Item.Logo
                    });
                model.PrimaryMainRuneId = item.MainChampionRunes.PrimaryMainRune.Id;
                model.PrimaryLevel1RuneId = item.MainChampionRunes.PrimaryRune1.Id;
                model.PrimaryLevel2RuneId = item.MainChampionRunes.PrimaryRune2.Id;
                model.PrimaryLevel3RuneId = item.MainChampionRunes.PrimaryRune3.Id;
                model.SecondaryLevel1RuneId = item.MainChampionRunes.SecondaryRune1.Id;
                model.SecondaryLevel2RuneId = item.MainChampionRunes.SecondaryRune2.Id;
                model.SecondaryLevel3RuneId = item.MainChampionRunes.SecondaryRune3.Id;
                model.PrimaryRuneCategoryId = item.MainChampionRunes.PrimaryMainRune.RuneCategory.Id;
                model.SecondaryRuneCategoryId = item.MainChampionRunes.SecondaryRune1.RuneCategory.Id;
            }
            AddAllCollections(model, cid);

            ViewData["cid"] = cid;
            return View(model);
        }


        // POST: ItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(int cid, MatchUpViewModel model)
        {
            var ids = model.ItemIDs?
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToList() ?? new List<int>();
            if (ModelState.IsValid)
            {
                var item = _context.MatchUps
                .Include(x => x.VsChampion)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryMainRune)
                        .ThenInclude(y => y.RuneCategory)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryRune1)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryRune2)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.PrimaryRune3)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.SecondaryRune1)
                        .ThenInclude(y => y.RuneCategory)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.SecondaryRune2)
                .Include(x => x.MainChampionRunes)
                    .ThenInclude(x => x.SecondaryRune3)
                .Include(x => x.ItemSets)
                    .ThenInclude(x => x.Item)
                .FirstOrDefault(x => x.Id == model.Id);
                if (item == null)
                {
                    item = new Data.Models.MatchUp()
                    {
                        MainChampionRunes = new Data.Models.ChampionRunes(),
                        ItemSets = new List<ItemSet>()
                    };
                    _context.MatchUps.Add(item);
                }

                item.WinRate = model.WinRate;
                item.StrongerEarly = model.StrongerEarly;
                item.StrongerMid = model.StrongerMid;
                item.StrongerLate = model.StrongerLate;
                item.MainChampion = _context.Champions.Where(x => x.Id == cid).FirstOrDefault();
                item.VsChampion = _context.Champions.Where(x => x.Id == model.VsChampionId).FirstOrDefault();
                item.MainChampionRunes.PrimaryMainRune = _context.MainRunes.Where(x => x.Id == model.PrimaryMainRuneId).FirstOrDefault();
                item.MainChampionRunes.PrimaryRune1 = _context.Runes.Where(x => x.Id == model.PrimaryLevel1RuneId).FirstOrDefault();
                item.MainChampionRunes.PrimaryRune2 = _context.Runes.Where(x => x.Id == model.PrimaryLevel2RuneId).FirstOrDefault();
                item.MainChampionRunes.PrimaryRune3 = _context.Runes.Where(x => x.Id == model.PrimaryLevel3RuneId).FirstOrDefault();
                item.MainChampionRunes.SecondaryRune1 = _context.Runes.Where(x => x.Id == model.SecondaryLevel1RuneId).FirstOrDefault();
                item.MainChampionRunes.SecondaryRune2 = _context.Runes.Where(x => x.Id == model.SecondaryLevel2RuneId).FirstOrDefault();
                item.MainChampionRunes.SecondaryRune3 = _context.Runes.Where(x => x.Id == model.SecondaryLevel3RuneId).FirstOrDefault();
                
                _context.ItemSets.RemoveRange(item.ItemSets.Where(x => !ids.Contains(x.Item.Id)));
                var newItemSets = ids.Where(x => !item.ItemSets.Any(y => y.Item.Id == x)).Select(x => new ItemSet
                {
                    Item = _context.Items.FirstOrDefault(y => y.Id == x)
                });
                foreach (var itemSet in newItemSets)
                {
                    item.ItemSets.Add(itemSet);
                }
                for (int i = 0; i < ids.Count(); i++)
                {
                    item.ItemSets.First(x => x.Item.Id == ids[i]).Sort = i;
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { cid });

            }
            model.Items = _context.ItemSets
                .Where(x => ids.Contains(x.Item.Id))
                .OrderBy(x => x.Sort)
                .Select(x => new ItemViewModel
                {
                    Id = x.Item.Id,
                    Name = x.Item.Name,
                    Description = x.Item.Description,
                    ImageSrc = x.Item.Logo
                });

            AddAllCollections(model, cid);

            ViewData["cid"] = cid;
            return View(model);
        }

        // GET: ItemsController/Delete/5
        public ActionResult Delete(int cid, int id)
        {
            var item = _context.MatchUps.Include(x => x.ItemSets).Include(x => x.MainChampionRunes).FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _context.ItemSets.RemoveRange(item.ItemSets);
            _context.ChampionRunes.Remove(item.MainChampionRunes);
            _context.MatchUps.Remove(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), new { cid });
        }

        public ActionResult GetItems(string q)
        {
            var res = new
            {
                success = true,
                results = _context.Items
                    .Where(x => x.Name.Contains(q))
                    .Select(x => new { id = x.Id, title = "<img src='" + x.Logo + "' /> " + x.Name })
            };
            return Ok(res);
        }

        private void AddAllCollections(MatchUpViewModel model, int cid)
        {
            model.AllChampions = _context.Champions.Where(x => x.Id != cid).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            model.AllRuneCategories = _context.RuneCategories.Select(x => new RuneCategoriesViewModel
            {
                Id = x.Id,
                ImageSrc = x.Logo,
                Description = x.Description
            }).ToList();
            model.AllMainRunes = _context.MainRunes
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.Sort)
                .Select(x => new MainRunesViewModel
                {
                    Id = x.Id,
                    RuneCategoryId = x.RuneCategory.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();

            #region Rune Levels
            model.AllLevel1Runes = _context.Runes
                .Where(x => x.LevelRune == 1)
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.Sort)
                .Select(x => new RuneViewModel
                {
                    Id = x.Id,
                    RuneCategoryId = x.RuneCategory.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();

            model.AllLevel2Runes = _context.Runes
                .Where(x => x.LevelRune == 2)
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.Sort)
                .Select(x => new RuneViewModel
                {
                    Id = x.Id,
                    RuneCategoryId = x.RuneCategory.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();

            model.AllLevel3Runes = _context.Runes
                .Where(x => x.LevelRune == 3)
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.Sort)
                .Select(x => new RuneViewModel
                {
                    Id = x.Id,
                    RuneCategoryId = x.RuneCategory.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();
            #endregion


            model.AllSecondaryRuneCategories = _context.RuneCategories
                .Select(x => new RuneCategoriesViewModel
                {
                    Id = x.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();



            model.AllLevel1SecondaryRunes = _context.Runes
                .Where(x => x.LevelRune == 1)
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.Sort)
                .Select(x => new RuneViewModel
                {
                    Id = x.Id,
                    RuneCategoryId = x.RuneCategory.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();

            model.AllLevel2SecondaryRunes = _context.Runes
                .Where(x => x.LevelRune == 2)
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.Sort)
                .Select(x => new RuneViewModel
                {
                    Id = x.Id,
                    RuneCategoryId = x.RuneCategory.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();

            model.AllLevel3SecondaryRunes = _context.Runes
                .Where(x => x.LevelRune == 3)
                .Include(x => x.RuneCategory)
                .OrderBy(x => x.Sort)
                .Select(x => new RuneViewModel
                {
                    Id = x.Id,
                    RuneCategoryId = x.RuneCategory.Id,
                    ImageSrc = x.Logo,
                    Description = x.Description
                }).ToList();
        }
    }
}
