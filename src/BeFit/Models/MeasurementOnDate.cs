using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models
{
    public class MeasurementOnDate
    {
        public int MeasurementOnDateID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }//дата измерения
        public int AppUserID{ get; set; }// ID пользователя
        public AppUser AppUser{ get; set; } // пользователь
        public ICollection<FillMeasurement> Measuarements { get; set; }// измерения на дату

    }
}
