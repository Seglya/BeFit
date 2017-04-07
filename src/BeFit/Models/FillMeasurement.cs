using System;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class FillMeasurement
    {
        public int FillMeasurementID { get; set; }
        public double PutMesurement { get; set; } // Числовое значение измерение
        public int MeasurementID { get; set; } // ID измерения
        public Measurement Measurement { get; set; } //Измерение

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } //дата измерения
        public int AppUserID { get; set; } // ID пользователя
        public AppUser AppUser { get; set; } // пользователь
    }
}