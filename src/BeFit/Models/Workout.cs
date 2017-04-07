using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Workout
    {
        public int WorkoutID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Название тренировки

        [DisplayFormat(NullDisplayText = "No Discription")]
        public string Description { get; set; } //Описание тренировки
        public bool PersonWorkout { get; set; }
        public int TagID { get; set; } //ID tags for search
        public Tag Tag { get; set; } //tags for search
        public ICollection<OneDayWorkout> OneDayWorkouts { get; set; }
        public ICollection<FillingWorkout> Exercises { get; set; } //Упражнения входящие в тренировку
    }
}