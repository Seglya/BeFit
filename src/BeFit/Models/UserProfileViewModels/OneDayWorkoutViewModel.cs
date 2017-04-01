using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models.WorkoutViewModels;

namespace BeFit.Models.UserProfileViewModels
{
    public class OneDayWorkoutViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // дата тренировки
        public int AppUserID { get; set; } // ID пользователя
        public int? CardioID { get; set; } //ID кардио
       public int Duration { get; set; } // продолжительность в минутах
        public int? WorkoutID { get; set; } //ID силовой тренировки
        public WorkoutViewModel WorkoutViewModel { get; set; } // силовая тренировка
    }
}
