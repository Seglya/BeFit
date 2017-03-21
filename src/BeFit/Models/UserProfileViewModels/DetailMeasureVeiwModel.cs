using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.UserProfileViewModels
{
    public class DetailMeasureVeiwModel
    {
        public  DateTime Date { get; set; }
        public List<Measure> Measure { get; set; }
    }

    public class Measure
    {
        public int idMeasure { get; set; }
        public double digit { get; set; }
    }

   
}
