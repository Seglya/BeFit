using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models
{
    public class WeightOfFood
    {
        public int WeightOfFoodID { get; set; }
        public double Weight { get; set; } //вес продукта
        public Food Food { get; set; } //продукт
        public int FoodID { get; set; } //ID продукта
    }
}
