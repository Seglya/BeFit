using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models
{
    public class Workout
    {
        //public Workout() { this.Exercises = new HashSet<Exercise>(); }
        public int WorkoutID { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; }//Название тренировки
        [DisplayFormat(NullDisplayText = "No Discription")]
        public string Description { get; set; }//Описание тренировки
        public bool PersonWorkout { get; set; }
        public string Tag { get; set; }//filter for search
        public ICollection< OneDayWorkout> OneDayWorkouts { get; set; }
        public ICollection<FillingWorkout> Exercises { get; set; }//Упражнения входящие в тренировку
    }
}
