using QRMenu.Areas.Admin.ViewModels;

namespace QRMenu.Areas.Client.ViewModels
{
    public class ProductViewModel
    {

        public ProductViewModel(string name, string description, float price, string fileNameInSystem, List<CategoryViewModel>? category)
        {

            Name = name;
            Description = description;
            Price = price;
            FileNameInSystem = fileNameInSystem;
            Category = category;
        }


        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string FileNameInSystem { get; set; }
        public List<CategoryViewModel>? Category { get; set; }
    }
}
