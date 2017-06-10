using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.UserProfileViewModels
{
    public class OneDayFoodViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // дата приема пищи
        public int AppUserID { get; set; } // ID пользователя
        [Range(0, 10)]
        public double Water { get; set; } //кол-во воды
        public ICollection<IngestionViewModel> Ingestions { get; set; } //приемы пищи

    }
}
