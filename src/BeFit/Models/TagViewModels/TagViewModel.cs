using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.TagViewModels
{
    public class TagViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
