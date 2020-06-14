using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class RuneCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public ICollection<MainRunes> MainRunes { get; set; }
        public ICollection<Runes> Runes { get; set; }
    }
}
