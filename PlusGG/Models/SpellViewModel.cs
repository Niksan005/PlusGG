using Microsoft.AspNetCore.Http;
using PlusGG.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Models
{
    public class SpellViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string SpellType { get; set; }

        public IFormFile Image { get; set; }
        public string ImageSrc { get; internal set; }
    }
}
