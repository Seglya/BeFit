using System;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class OneDayWorkout
    {
        public int OneDayWorkoutID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // дата тренировки
        public int AppUserID { get; set; } // ID пользователя
        public AppUser AppUser { get; set; } // пользователь
        public int? CardioID { get; set; } //ID кардио
        public Cardio Cardio { get; set; } //кардто тренировка
        public int Duration { get; set; } // продолжительность в минутах
        public int? WorkoutID { get; set; } //ID силовой тренировки
        public virtual Workout Workout { get; set; } // силовая тренировка
    }
}