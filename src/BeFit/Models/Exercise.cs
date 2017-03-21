using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Exercise
    {
        public int ExerciseID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\-\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Название упражнения
        public ICollection<GroupsOfMuscles> Muscles { set; get; } //Группы мышц

        [DisplayFormat(NullDisplayText = "No Discription")]
        public string Description { get; set; } //Описание упражнения
        public byte[] ImageData { set; get; } // Изображение

        public string ImageMimeType { set; get; } // Mime тип изображения 
        public ICollection<FillingWorkout> Workouts { get; set; }
    }
}