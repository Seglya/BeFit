using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Service
    {
        //public Service(){ this.Trainers = new HashSet<Trainer>(); }
        public int ServiceID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Имя Услуги

        [DisplayFormat(NullDisplayText = "No Discription")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(500, ErrorMessage = "Name can't be longer than 500 characters!")]
        public string Description { get; set; } //описание услуги
        public byte[] ImageData { set; get; } // Изображение
        public string ImageMimeType { set; get; } // Mime тип изображения 
        public ICollection<TrainersServices> Trainers { get; set; } //тренера оказывающие выбранную услугу
    }
}