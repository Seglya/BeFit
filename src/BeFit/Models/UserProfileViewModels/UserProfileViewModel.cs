using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BeFit.Models.UserProfileViewModels
{
    public class UserProfileViewModel
    {
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

        [Required]
        public string Sex { get; set; } //Пол пользователя
        public string ImageName { set; get; } //Имя Изображения
        public string ImagePath { set; get; } // Месторасположение изображения 

        [DataType(DataType.Upload)]
        public IFormFile IFile { get; set; } //Изображение

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } //Дата рождения

        [DataType(DataType.Date)]
        public DateTime DateOfRegisrtration { get; set; } //Дата рождения

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return SecondName + ", " + FirstName; }
        }

        [Range(30, 300)]
        public double Goal { get; set; } //цель в кг
        public int WeeksForGoal { get; set; } // срок достижения цели
        public int Height { get; set; }

        [Range(30, 300)]
        public double CurrentWeight { get; set; } //Текуший вес
        public double Activity { get; set; }
        public int Years => DateTime.Today.Year - DateOfBirth.Year;

        [Display(Name = "Base calories per day")]
        public double BaseCal
        {
            get
            {
                double t=1;
                if (Sex.Equals("Female"))
                  t=447.6 + (9.2 * CurrentWeight) + (3.1 * Height) - (4.3 * Years) * Activity;
                
                else
                {
                    t=(88.36 + (13.4 * CurrentWeight) + (4.8 * Height) - (5.7 * Years)) * Activity;
                }
                return t;
            }
        }

        [Display(Name = "Calorie deficit per day to reach your goal")]
        public double ReachGoal => (CurrentWeight-Goal)*7700/(WeeksForGoal*7);
    }
}