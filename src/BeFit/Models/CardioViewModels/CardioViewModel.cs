using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.CardioViewModels
{
    public class CardioViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\d\/\-\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } // Название кардио

        public double CalPerHour { get; set; }
        //public ICollection<OneDayWorkout> CardioWorkouts { get; set; }
    }
}
