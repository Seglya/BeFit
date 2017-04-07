using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Tag
    {
        public int TagID { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\-\\s]*")]
        [StringLength(15, ErrorMessage = "Name can't be longer than 15 characters!")]
        public string Name { get; set; }

        public ICollection<Workout> Workouts { get; set; }
    }
}