using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models
{
    public class FillMeasurement
    {
        public int FillMeasurementID { get; set; }
        public double PutMesurement { get; set; } // Числовое значение измерение
        public int MeasurementID { get; set; }// ID измерения
        public Measurement Measurement { get; set; } //Измерение
        public int MeasurementOnDateID { get; set; } //ID измерения по дате
        public MeasurementOnDate MeasurementOnDate { get; set; }// Измерение по дате
    }
}
