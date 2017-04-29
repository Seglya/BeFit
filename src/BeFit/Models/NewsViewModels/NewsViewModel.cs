using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.NewsViewModels
{
    public class NewsViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Название статьи
        public string Annatation { get; set; }
        public string[] Article { get; set; }
        public string ImagePath { set; get; } // Месторасположение изображения 
        public string Tag { get; set; }
    }
}
