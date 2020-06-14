using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class MainRunes
    {
        public int Id { get; set; }
        public RuneCategories RuneCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public int Sort { get; set; }
        public ICollection<ChampionRunes> ChampionRunes { get; set; }
    }
}
