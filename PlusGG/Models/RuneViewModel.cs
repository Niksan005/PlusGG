using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlusGG.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlusGG.Models
{
    public class RuneViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int Sort { get; set; }

        [Required]
        public int RuneCategoryId { get; set; }
        public string RuneCategoryName { get; set; }

        public IEnumerable<SelectListItem> AllRuneCategories { get; set; }

        public IFormFile Image { get; set; }
        public string ImageSrc { get; internal set; }
    }
}
