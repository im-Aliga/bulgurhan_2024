using System.ComponentModel.DataAnnotations;

namespace QRMenu.Areas.Admin.ViewModels
{
    public class CategoryAddViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
