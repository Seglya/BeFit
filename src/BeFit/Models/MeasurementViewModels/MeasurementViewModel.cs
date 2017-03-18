using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.MeasurementViewModels
{
    public class MeasurementViewModel
    {
    
    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\-\s\d]*$")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
    public string Name { get; set; }// Название измерения
    [Required]
    [RegularExpression(@"[a-zA-Z\.\/\s]*$")]
    [Display(Name = "Units")]
    public string UnitsOfMeasurement { get; set; } // единицы измерения
    }
}
