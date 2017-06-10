using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.UserProfileViewModels
{
    public class UserWorkoutViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\-\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Название тренировки
        public bool PersonWorkout { get; set; }
        public ICollection<UserExerciseViewModel> Exercises { get; set; } //Упражнения входящие в тренировку
      
    }
}
