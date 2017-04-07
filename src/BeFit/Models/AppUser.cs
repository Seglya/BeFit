using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class AppUser
    {
        public int AppUserID { get; set; }
        public string Key { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\-\s]*$")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } //имя пользователя

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\-\s]*$")]
        [StringLength(50, ErrorMessage = "Second Name can't be longer than 50 characters!")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; } //Фамилия пользователя
        public string Sex { get; set; } //Пол пользователя

        [RegularExpression(@"(\.+[(jpeg)(jpg)])$",
            ErrorMessage = "Invalid format of image. Extation must be .jpg or jpeg")]
        public string ImageName { set; get; } //Имя Изображения
        public string ImagePath { set; get; } // Месторасположение изображения 

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } //Дата рождения

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfRegoistration { get; set; } //Дата ренистрации

        [Display(Name = "Full Name")]
        public double Goal { get; set; } //цель в кг
        public int WeeksForGoal { get; set; } // срок достижения цели
        public int CurrentWeight { get; set; }
        public ICollection<OneDayFood> Food { get; set; }
        public ICollection<OneDayWorkout> Workouts { get; set; }
        public ICollection<FillMeasurement> Measurements { get; set; }
    }
}