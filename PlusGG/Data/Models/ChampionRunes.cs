using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class ChampionRunes
    {
        public int Id { get; set; }
        public MainRunes PrimaryMainRune { get; set; }
        public Runes PrimaryRune1 { get; set; }
        public Runes PrimaryRune2 { get; set; }
        public Runes PrimaryRune3 { get; set; }
        public Runes SecondaryRune1 { get; set; }
        public Runes SecondaryRune2 { get; set; }
        public Runes SecondaryRune3 { get; set; }
        public MatchUp MatchUp { get; set; }
    }
}
