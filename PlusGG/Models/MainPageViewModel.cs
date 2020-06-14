using Microsoft.AspNetCore.Http;
using PlusGG.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Models
{
    public class MainPageViewModel
    {
        public int? cid { get; set; }
        public int? vscid { get; set; }
        public ChampionViewModel Champion { get; set; }
        public MatchUpViewModel VsChampion { get; set; }
        public ICollection<MatchUpViewModel> AllVsChamps { get; set; }
    }
}
