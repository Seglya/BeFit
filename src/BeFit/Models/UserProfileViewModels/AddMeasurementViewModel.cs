using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.UserProfileViewModels
{
    public class AddMeasurementViewModel
    {
        public double PutMesurement { get; set; } // Числовое значение измерение
        public Measurement Measurement{ get; set; }// измерения
        public int MeasurementID { get; set; }//ID измерения
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }//дата измерения
        public int AppUserID { get; set; }// ID пользователя
        public int ID { get; set; }// ID 


    }
}
