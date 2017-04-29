using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class News
    {
        public int NewsID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters!")]
        public string Name { get; set; } //Название статьи

        [Required]
       public string Path { get; set; } //Расположение файла

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Tag { get; set; } //Ключевые слова
        public string ImagePath { get; set; } //Расположение изображения
    }
}