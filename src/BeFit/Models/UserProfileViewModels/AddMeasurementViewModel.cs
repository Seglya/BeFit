using System;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.UserProfileViewModels
{
    public class AddMeasurementViewModel
    {
        public double PutMesurement { get; set; } // Числовое значение измерение
        public Measurement Measurement { get; set; } // измерения
        public int MeasurementID { get; set; } //ID измерения

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } //дата измерения
        public int AppUserID { get; set; } // ID пользователя
        public int ID { get; set; } // ID 
    }
}