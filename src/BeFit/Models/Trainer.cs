using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Trainer
    {
        //public Trainer() { this.Services = new HashSet<Service>(); }
        public int TrainerID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } //имя тренера

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Second Name can't be longer than 50 characters!")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; } //Фамилия тренера
        public bool? PersonalTrainer { get; set; } //Является ли персональным тренером

        [DisplayFormat(NullDisplayText = "No Discription")]
        public string Discription { get; set; } // info about trainer
        public byte[] ImageData { set; get; } // Изображение
        public string ImageMimeType { set; get; } // Mime тип изображения 
        public virtual ICollection<TrainersServices> Services { get; set; } //услуги оказывающий выбранный тренер

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return SecondName + ", " + FirstName; }
        }
    }
}