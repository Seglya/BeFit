using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models.UserProfileViewModels;

namespace BeFit.Models
{
    public class CountMealPerIngestion
    {
        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarb { get; set; }
        public double TotalCal { get; set; }

        public static CountMealPerIngestion Count(IngestionViewModel viewModel)
        {var count=new CountMealPerIngestion();
            if (viewModel.WeightOfFood!=null)
            foreach (var weightOfFoodViewModel in viewModel.WeightOfFood)
            {
                    if (weightOfFoodViewModel.Food != null) { 
                count.TotalCal += weightOfFoodViewModel.Calories;
                count.TotalCarb += weightOfFoodViewModel.Carbohydrate;
                count.TotalFat += weightOfFoodViewModel.Fat;
                count.TotalProtein += weightOfFoodViewModel.Protein;}
                   
                
            }
            return count;
        }

        public static CountMealPerIngestion TotalCount(List<CountMealPerIngestion> list)
        {
            var count=new CountMealPerIngestion();
            foreach (var countMealPerIngestion in list)
            {
                count.TotalCarb += countMealPerIngestion.TotalCarb;
                count.TotalFat += countMealPerIngestion.TotalFat;
                count.TotalProtein += countMealPerIngestion.TotalProtein;
                count.TotalCal += countMealPerIngestion.TotalCal;
            }
            return count;
        }
    }
}
