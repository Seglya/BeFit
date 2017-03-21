namespace BeFit.Models
{
    public class GroupsOfMuscles
    {
        public int MuscleID { get; set; }
        public int ExerciseID { get; set; }
        public Exercise Exercise { get; set; }
        public Muscle Muscle { get; set; }
    }
}