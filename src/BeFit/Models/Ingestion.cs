using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models
{
    public class Ingestion
    {
       public int IngestionID { get; set; }
       public string Name { get; set; }//наименование приема пищи
        [DataType(DataType.Time)]
        public DateTime Time { get; set; } //время приема
        [Required]
        public WeightOfFood WeightOfFood { get; set; } //взвешенный продукт
        public int WeightOfFoodID { get; set; } //ID продукта
        [Required]
        public OneDayFood OneDayFood { get; set; }//день приема пищи
        public int OneDayFoodID { get; set; } //ID дня приема пищи

    }
}
