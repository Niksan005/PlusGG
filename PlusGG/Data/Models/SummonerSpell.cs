using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class SummonerSpell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public ICollection<Champion> ChampionD { get; set; }
        public ICollection<Champion> ChampionF { get; set; }
    }
}
