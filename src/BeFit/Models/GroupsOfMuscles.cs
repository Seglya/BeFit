using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
