namespace BeFit.Models
{
    public class FillingWorkout
    {
        public int FillingWorkoutID { get; set; }
        public int Sets { get; set; } //Количество сетов
        public int RepeatMin { get; set; } //Количество повторов минимальное
        public int RepeatMax { get; set; } //Количество повторов максимальное
        public int WorkoutID { get; set; }
        public int ExerciseID { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}