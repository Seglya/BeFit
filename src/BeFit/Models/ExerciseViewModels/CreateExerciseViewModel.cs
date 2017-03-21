using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BeFit.Models.ExerciseViewModels
{
    public class CreateExerciseViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\-\s]*$", ErrorMessage = "Name can't contain numbers or simbols!!!")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 10)]
        public string Description { get; set; }

        [Display(Name = "Group of muscles")]
        public ICollection<GroupsOfMuscles> Muscles { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile image { get; set; }

        public byte[] ImageData { get; set; }

        public IEnumerable<Muscle> AllMuscles { get; set; }
    }
}