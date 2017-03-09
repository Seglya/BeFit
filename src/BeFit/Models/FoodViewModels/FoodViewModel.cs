using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.FoodViewModels
{
    public class FoodViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; }
        [Range(0,1000)]
        public int Calories { get; set; } //Каллорийность блюда
        [Range(0,1000)]
        public double Fat { get; set; }// Количество жира
        [Range(0, 1000)]
        public double Protein { get; set; } //Количество белка
        [Range(0, 1000)]
        public double Carbohydrate { get; set; }//Количество углеводов
    }
}
