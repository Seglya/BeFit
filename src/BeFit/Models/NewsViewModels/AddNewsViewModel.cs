using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BeFit.Models.NewsViewModels
{
    public class AddNewsViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Название статьи

       
        [StringLength(50, ErrorMessage = "Path can't be longer than 50 characters!")]
        public string Path { get; set; } //Расположение файла

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Tag { get; set; } //Ключевые слова
        public string ImagePath { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile IImageFile { get; set; } //Изображение
        [DataType(DataType.Upload)]
        //[FileExtensions(ErrorMessage = "Choose a .txt or .doc file",Extensions ="doc,txt,docx")]
        public IFormFile ITextFile { get; set; } //Текст
    }
}
