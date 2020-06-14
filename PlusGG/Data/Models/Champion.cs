using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class Champion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SummonerSpell SummonerSpellD { get; set; }
        public SummonerSpell SummonerSpellF { get; set; }
        public ICollection<Spell> Spells { get; set; }
        public string Logo { get; set; }
        public ICollection<MatchUp> MatchUpsMain { get; set; }
        public ICollection<MatchUp> MatchUpsVs { get; set; }

    }
}
