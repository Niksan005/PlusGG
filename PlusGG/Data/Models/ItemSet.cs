using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Data.Models
{
    public class ItemSet
    {
        public int Id { get; set; }
        public MatchUp MatchUp { get; set; }
        public Item Item { get; set; }
        public int Sort { get; set; }
    }
}
