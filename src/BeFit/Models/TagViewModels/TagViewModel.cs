using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.TagViewModels
{
    public class TagViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}