using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.UserProfileViewModels
{
    public class MealDetailViewModel
    {
        public OneDayFoodViewModel OneDayFoodViewModel { get; set; }
        public List<CountMealPerIngestion> CountMealPerIngestions { get; set; }
        public CountMealPerIngestion TotalCountMealPerIngestion =>CountMealPerIngestion.TotalCount(CountMealPerIngestions);
        public int OneDayFoodId { get; set; }
    }
}
