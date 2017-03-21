using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFit.Models
{
    public class Subscription
    {
        public int SubscriptionID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Название абонемента

        [Required]
        [Display(Name = "Count of Visits")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(20, ErrorMessage = "Count of visits can't be longer than 50 characters!")]
        public string CountOfVisits { get; set; } // Количество посещений

        [Display(Name = "Duration, month")]
        public int DurationMonth { get; set; } //срок действия

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; } //цена абонемента

        [Range(0, 3)]
        public int TypeService { get; set; } //Типы на что распространяется абонемент
    }
}