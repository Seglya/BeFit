using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.UserProfileViewModels
{
    public class UserExerciseViewModel
    {
        public int Sets { get; set; } //Количество сетов

       [Range(1, 100, ErrorMessage = "Count of max repeat should be 1-100")]
        public int Repeat { get; set; } //Количество повторов 
        public string NameExercise { get; set; }
        public int WorkoutID { get; set; }
        public int ExerciseID { get; set; }
       
    }
}
