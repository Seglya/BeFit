using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Muscle
    {
        public int MuscleID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } // наименование мышцы
        public ICollection<GroupsOfMuscles> GroupsOfMuscles { get; set; } // ы какие нруппы мышц входит
    }
}