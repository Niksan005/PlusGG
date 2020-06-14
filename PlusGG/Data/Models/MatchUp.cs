using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class MatchUp
    {
        public int Id { get; set; }
        public Champion MainChampion { get; set; }
        public Champion VsChampion { get; set; }
        public ChampionRunes MainChampionRunes { get; set; }
        public double WinRate { get; set; }
        public bool StrongerEarly { get; set; }
        public bool StrongerMid { get; set; }
        public bool StrongerLate { get; set; }
        public ICollection<ItemSet> ItemSets { get; set; }
    }
}
