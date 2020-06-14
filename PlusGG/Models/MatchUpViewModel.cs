using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlusGG.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Models
{
    public class MatchUpViewModel
    {
        public int Id { get; set; }
        public int? VsChampionId { get; set; }
        public ChampionViewModel VsChampion { get; set; }
        public string ItemIDs { get; set; }
        public IEnumerable<ItemViewModel> Items { get; internal set; }
        public IEnumerable<SelectListItem> AllChampions { get; set; }

        [Display(Name = "Rune Tree")]
        public int PrimaryRuneCategoryId { get; set; }
        public IEnumerable<RuneCategoriesViewModel> AllRuneCategories { get; set; }


        [Display(Name = "Main Rune")]
        public int PrimaryMainRuneId { get; set; }
        public IEnumerable<MainRunesViewModel> AllMainRunes { get; set; }


        [Display(Name = "Level 1 Rune")]
        public int PrimaryLevel1RuneId { get; set; }
        public IEnumerable<RuneViewModel> AllLevel1Runes { get; set; }


        [Display(Name = "Level 2 Rune")]
        public int PrimaryLevel2RuneId { get; set; }
        public IEnumerable<RuneViewModel> AllLevel2Runes { get; set; }


        [Display(Name = "Level 3 Rune")]
        public int PrimaryLevel3RuneId { get; set; }
        public IEnumerable<RuneViewModel> AllLevel3Runes { get; set; }



        [Display(Name = "Secondary Rune Tree")]
        public int SecondaryRuneCategoryId { get; set; }
        public IEnumerable<RuneCategoriesViewModel> AllSecondaryRuneCategories { get; set; }


        [Display(Name = "Level 1 Rune")]
        public int SecondaryLevel1RuneId { get; set; }
        public IEnumerable<RuneViewModel> AllLevel1SecondaryRunes { get; set; }


        [Display(Name = "Level 2 Rune")]
        public int SecondaryLevel2RuneId { get; set; }
        public IEnumerable<RuneViewModel> AllLevel2SecondaryRunes { get; set; }


        [Display(Name = "Level 3 Rune")]
        public int SecondaryLevel3RuneId { get; set; }
        public IEnumerable<RuneViewModel> AllLevel3SecondaryRunes { get; set; }

        public double WinRate { get; set; }


        public bool StrongerEarly { get; set; }
        public bool StrongerMid { get; set; }
        public bool StrongerLate { get; set; }
    }
}
