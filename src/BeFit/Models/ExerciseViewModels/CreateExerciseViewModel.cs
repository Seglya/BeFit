﻿using BeFit.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models.ExerciseViewModels
{
    public class CreateExerciseViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\-\s]*$",ErrorMessage ="Name can't contain numbers or simbols!!!")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; }
        [Required]
        [StringLength(10000,MinimumLength =10)]
        public string Description { get; set; }
        [Required]
        [Display(Name ="Group of muscles")]
        public ICollection<Muscle> Muscles { get; set; } 
        [DataType(DataType.Upload)]
       [FileExtensions(ErrorMessage ="Image should have extension .jpg",Extensions = "jpeg")]
        public IFormFile image { get; set; }
        public IMusclesRepository _rep { get ; }
    }
}