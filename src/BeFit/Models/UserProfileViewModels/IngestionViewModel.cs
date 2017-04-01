using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.UserProfileViewModels
{
    public class IngestionViewModel
    {
        [Required]
        public string Name { get; set; } //наименование приема пищи
        public int OneDayFoodId { get; set; }
      
        public ICollection<WeightOfFoodViewModel> WeightOfFood { get; set; } //взвешенный продукт

        public static ICollection<IngestionViewModel> OneDayIngestions => new Collection<IngestionViewModel>
        {
            new IngestionViewModel
            {
                Name = "Breakfast"
            },
            new IngestionViewModel {Name = "Second Breakfast"},
            new IngestionViewModel {Name = "Lunch"},
            new IngestionViewModel {Name = "Snack"},
            new IngestionViewModel {Name = "Dinner"}
        };
        public CountMealPerIngestion GetCountMealPerIngestion =>CountMealPerIngestion.Count(this); 
    }
}
