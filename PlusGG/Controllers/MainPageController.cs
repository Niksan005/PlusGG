using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlusGG.Data;
using PlusGG.Models;

namespace PlusGG.Controllers
{
    public class MainPageController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MainPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemsController
        public ActionResult Index(int? cid, int? vscid)
        {
            ChampionViewModel mainChamp = null;
            MatchUpViewModel vscVM = null;
            if (cid.HasValue)
            {
                mainChamp = _context.Champions
                    .Include(x => x.Spells)
                    .Include(x => x.SummonerSpellD)
                    .Include(x => x.SummonerSpellF)
                    .Where(x => x.Id == cid)
                    .Select(x => new ChampionViewModel
                    {
                        Id = x.Id,
                        ImageSrc = x.Logo,
                        Name = x.Name,
                        SpellP = x.Spells.Where(s => s.SpellType == "P").Select(s => new SpellViewModel
                        {
                            Name = s.Name,
                            Description = s.Description,
                            ImageSrc = s.Logo
                        }).FirstOrDefault(),
                        SpellQ = x.Spells.Where(s => s.SpellType == "Q").Select(s => new SpellViewModel
                        {
                            Name = s.Name,
                            Description = s.Description,
                            ImageSrc = s.Logo
                        }).FirstOrDefault(),
                        SpellW = x.Spells.Where(s => s.SpellType == "W").Select(s => new SpellViewModel
                        {
                            Name = s.Name,
                            Description = s.Description,
                            ImageSrc = s.Logo
                        }).FirstOrDefault(),
                        SpellE = x.Spells.Where(s => s.SpellType == "E").Select(s => new SpellViewModel
                        {
                            Name = s.Name,
                            Description = s.Description,
                            ImageSrc = s.Logo
                        }).FirstOrDefault(),
                        SpellR = x.Spells.Where(s => s.SpellType == "R").Select(s => new SpellViewModel
                        {
                            Name = s.Name,
                            Description = s.Description,
                            ImageSrc = s.Logo
                        }).FirstOrDefault(),
                        SummonerSpellD = new SummonerSpellViewModel
                        {
                            Name = x.SummonerSpellD.Name,
                            Description = x.SummonerSpellD.Description,
                            ImageSrc = x.SummonerSpellD.Logo
                        },
                        SummonerSpellF = new SummonerSpellViewModel
                        {
                            Name = x.SummonerSpellF.Name,
                            Description = x.SummonerSpellF.Description,
                            ImageSrc = x.SummonerSpellF.Logo
                        },
                        AllSummonerSpells = _context.SummonerSpells.Select(s => new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = x.Name
                        }).ToList(),

                    })
                    .FirstOrDefault();
                var matchUp = _context.MatchUps
                   .Include(x => x.MainChampionRunes)
                       .ThenInclude(x => x.PrimaryMainRune)
                           .ThenInclude(x => x.RuneCategory)
                   .Include(x => x.MainChampionRunes)
                       .ThenInclude(x => x.PrimaryRune1)
                           .ThenInclude(x => x.RuneCategory)
                   .Include(x => x.MainChampionRunes)
                       .ThenInclude(x => x.PrimaryRune2)
                   .Include(x => x.MainChampionRunes)
                       .ThenInclude(x => x.PrimaryRune3)
                   .Include(x => x.MainChampionRunes)
                       .ThenInclude(x => x.SecondaryRune1)
                           .ThenInclude(x => x.RuneCategory)
                   .Include(x => x.MainChampionRunes)
                       .ThenInclude(x => x.SecondaryRune2)
                   .Include(x => x.MainChampionRunes)
                       .ThenInclude(x => x.SecondaryRune3)
                   .Include(x => x.ItemSets)
                        .ThenInclude(x => x.Item)
                   .Include(x => x.VsChampion)
                   .Where(x => x.MainChampion.Id == cid && ((!vscid.HasValue && x.VsChampion == null) || (x.VsChampion.Id == vscid)))
                   .FirstOrDefault();
                vscVM = new MatchUpViewModel
                {
                    Id = matchUp.Id,
                    VsChampionId = vscid,
                    VsChampion = matchUp.VsChampion == null ? null : new ChampionViewModel { 
                        ImageSrc = matchUp.VsChampion.Logo,
                        Name = matchUp.VsChampion.Name
                    },
                    PrimaryRuneCategoryId = matchUp.MainChampionRunes.PrimaryMainRune.RuneCategory.Id,
                    SecondaryRuneCategoryId = matchUp.MainChampionRunes.SecondaryRune1.RuneCategory.Id,
                    PrimaryMainRuneId = matchUp.MainChampionRunes.PrimaryMainRune.Id,
                    PrimaryLevel1RuneId = matchUp.MainChampionRunes.PrimaryRune1.Id,
                    PrimaryLevel2RuneId = matchUp.MainChampionRunes.PrimaryRune2.Id,
                    PrimaryLevel3RuneId = matchUp.MainChampionRunes.PrimaryRune3.Id,
                    SecondaryLevel1RuneId = matchUp.MainChampionRunes.SecondaryRune1.Id,
                    SecondaryLevel2RuneId = matchUp.MainChampionRunes.SecondaryRune2.Id,
                    SecondaryLevel3RuneId = matchUp.MainChampionRunes.SecondaryRune3.Id,
                    StrongerEarly = matchUp.StrongerEarly,
                    StrongerMid = matchUp.StrongerMid,
                    StrongerLate = matchUp.StrongerLate,
                    Items = matchUp.ItemSets.Select(y => new ItemViewModel
                    {
                        Name = y.Item.Name,
                        ImageSrc = y.Item.Logo,
                        Description = y.Item.Description
                    }),
                    AllRuneCategories = _context.RuneCategories.Select(y => new RuneCategoriesViewModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Description = y.Description,
                        ImageSrc = y.Logo,
                    }),
                    AllSecondaryRuneCategories = _context.RuneCategories.Select(y => new RuneCategoriesViewModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Description = y.Description,
                        ImageSrc = y.Logo,
                    }),
                    AllMainRunes = _context.MainRunes
                           .Where(y => y.RuneCategory.Id == matchUp.MainChampionRunes.PrimaryMainRune.RuneCategory.Id)
                           .OrderBy(y => y.Sort)
                           .Select(y => new MainRunesViewModel
                           {
                               Id = y.Id,
                               Name = y.Name,
                               Description = y.Description,
                               ImageSrc = y.Logo,
                           }).ToList(),
                    AllLevel1Runes = _context.Runes
                           .Where(y => y.LevelRune == 1 && y.RuneCategory.Id == matchUp.MainChampionRunes.PrimaryMainRune.RuneCategory.Id)
                           .OrderBy(y => y.Sort)
                           .Select(y => new RuneViewModel
                           {
                               Id = y.Id,
                               Name = y.Name,
                               Description = y.Description,
                               ImageSrc = y.Logo,
                           }),
                    AllLevel2Runes = _context.Runes
                           .Where(y => y.LevelRune == 2 && y.RuneCategory.Id == matchUp.MainChampionRunes.PrimaryMainRune.RuneCategory.Id)
                           .OrderBy(y => y.Sort)
                           .Select(y => new RuneViewModel
                           {
                               Id = y.Id,
                               Name = y.Name,
                               Description = y.Description,
                               ImageSrc = y.Logo,
                           }),
                    AllLevel3Runes = _context.Runes
                            .Where(y => y.LevelRune == 3 && y.RuneCategory.Id == matchUp.MainChampionRunes.PrimaryMainRune.RuneCategory.Id)
                            .OrderBy(y => y.Sort)
                            .Select(y => new RuneViewModel
                            {
                                Id = y.Id,
                                Name = y.Name,
                                Description = y.Description,
                                ImageSrc = y.Logo,
                            }),
                    AllLevel1SecondaryRunes = _context.Runes
                            .Where(y => y.LevelRune == 1 && y.RuneCategory.Id == matchUp.MainChampionRunes.SecondaryRune1.RuneCategory.Id)
                            .OrderBy(y => y.Sort)
                            .Select(y => new RuneViewModel
                            {
                                Id = y.Id,
                                Name = y.Name,
                                Description = y.Description,
                                ImageSrc = y.Logo,
                            }),
                    AllLevel2SecondaryRunes = _context.Runes
                            .Where(y => y.LevelRune == 2 && y.RuneCategory.Id == matchUp.MainChampionRunes.SecondaryRune1.RuneCategory.Id)
                            .OrderBy(y => y.Sort)
                            .Select(y => new RuneViewModel
                            {
                                Id = y.Id,
                                Name = y.Name,
                                Description = y.Description,
                                ImageSrc = y.Logo,
                            }),
                    AllLevel3SecondaryRunes = _context.Runes
                            .Where(y => y.LevelRune == 3 && y.RuneCategory.Id == matchUp.MainChampionRunes.SecondaryRune1.RuneCategory.Id)
                            .OrderBy(y => y.Sort)
                            .Select(y => new RuneViewModel
                            {
                                Id = y.Id,
                                Name = y.Name,
                                Description = y.Description,
                                ImageSrc = y.Logo,
                     }),

                };
            }
            MainPageViewModel model = new MainPageViewModel
            {
                cid = cid,
                vscid = vscid,
                Champion = mainChamp,
                VsChampion = vscVM,
                AllVsChamps = _context.MatchUps.Include(x => x.VsChampion).Where(x => x.MainChampion.Id == cid).Select(x => new MatchUpViewModel
                {
                    WinRate = x.WinRate,
                    VsChampion = new ChampionViewModel
                    {
                        Id = x.VsChampion.Id,
                        Name = x.VsChampion.Name,
                        ImageSrc = x.VsChampion.Logo
                    }
                }).ToList()
            };


            return View(model);
        }

        public ActionResult GetMainChampoin(string q)
        {
            var res = new
            {
                success = true,
                results = _context.Champions
                    .Where(x => x.Name.Contains(q))
                    .Select(x => new { link = Url.Action("Index", "MainPage", new { cid = x.Id }), title = "<img src='" + x.Logo + "' /> " + x.Name })
            };
            return Ok(res);
        }


        public ActionResult GetVsChampion(string q, int cid)
        {
            var res = new
            {
                success = true,
                results = _context.MatchUps
                    .Include(x => x.VsChampion)
                    .Where(x => x.VsChampion.Name.Contains(q) && x.MainChampion.Id == cid)
                    .Select(x => new { link = Url.Action("Index", "MainPage", new { cid = cid, vscid = x.VsChampion.Id }), title = "<img src='" + x.VsChampion.Logo + "' /> " + x.VsChampion.Name })
            };
            return Ok(res);
        }
    }
}
