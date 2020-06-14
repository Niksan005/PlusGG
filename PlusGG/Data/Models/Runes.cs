using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class Runes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LevelRune { get; set; }
        public int Sort { get; set; }
        public string Logo { get; set; }
        public RuneCategories RuneCategory { get; set; }
        public ICollection<ChampionRunes> PrimaryRune1 { get; set; }
        public ICollection<ChampionRunes> PrimaryRune2 { get; set; }
        public ICollection<ChampionRunes> PrimaryRune3 { get; set; }
        public ICollection<ChampionRunes> SecondaryRune1 { get; set; }
        public ICollection<ChampionRunes> SecondaryRune2 { get; set; }
        public ICollection<ChampionRunes> SecondaryRune3 { get; set; }
    }
}
