using System.Collections.Generic;


namespace BeFit.Models
{
    public class Ingestion
    {
        public int IngestionID { get; set; }
        public string Name { get; set; } //наименование приема пищи
        public ICollection<WeightOfFood> WeightOfFood { get; set; } //взвешенный продукт
        public OneDayFood OneDayFood { get; set; } //день приема пищи
        public int OneDayFoodID { get; set; } //ID дня приема пищи
        
    }
}