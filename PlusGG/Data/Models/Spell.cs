using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string SpellType { get; set; }
        public Champion Champion { get; set; }
    }
}
