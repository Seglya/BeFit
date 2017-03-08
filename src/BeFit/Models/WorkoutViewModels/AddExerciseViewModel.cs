using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.WorkoutViewModels
{
    public class AddExerciseViewModel
    {
      
        public int Sets { get; set; } //Количество сетов
        [Range(1, 100,ErrorMessage = "Count of min repeat should be 1-100")]
        public int RepeatMin { get; set; }//Количество повторов минимальное
        [Range(1, 100, ErrorMessage = "Count of max repeat should be 1-100")]
        public int RepeatMax { get; set; }//Количество повторов максимальное
        public string NameExercise { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
        public IEnumerable<Exercise> AllExercises { get; set; }
    }
}
