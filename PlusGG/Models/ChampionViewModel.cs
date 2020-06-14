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
    public class ChampionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SummonerSpellDId { get; set; }
        public int SummonerSpellFId { get; set; }

        public SummonerSpellViewModel SummonerSpellD { get; set; }
        public SummonerSpellViewModel SummonerSpellF { get; set; }

        public IEnumerable<SelectListItem> AllSummonerSpells { get; set; }

        public SpellViewModel SpellP { get; set; }
        public SpellViewModel SpellQ { get; set; }
        public SpellViewModel SpellW { get; set; }
        public SpellViewModel SpellE { get; set; }
        public SpellViewModel SpellR { get; set; }

        public IFormFile Image { get; set; }
        public string ImageSrc { get; internal set; }
    }
}
