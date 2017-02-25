using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; }
        public int Calories { get; set; } //Каллорийность блюда
        public double Fat { get; set; }// Количество жира
        public double Protein { get; set; } //Количество белка
        public double Carbohydrate { get; set; }//Количество углеводов
       public ICollection<WeightOfFood> WeightsOfFood { get; set; }
      
    }
}
