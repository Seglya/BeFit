using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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

        [Range(30, 300)]
        public int CurrentWeight { get; set; } //Текуший вес
    }
}