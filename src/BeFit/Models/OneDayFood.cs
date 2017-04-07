using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class OneDayFood
    {
        public int OneDayFoodID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // дата приема пищи
        public int AppUserID { get; set; } // ID пользователя
        public AppUser AppUser { get; set; } // пользователь
        [Range(0, 10)]
        public double Water { get; set; } //кол-во воды
        public ICollection<Ingestion> Ingestions { get; set; } //приемы пищи
    }
}