using QRMenu.Database.Models;

namespace QRMenu.Areas.Admin.ViewModels
{
    public class AddViewModel
    {
        public AddViewModel()
        {
            
        }

        public AddViewModel(int id, string name, string description, float price, string fileNameInSystem)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            FileNameInSystem = fileNameInSystem;
        }

        public AddViewModel(int id, string name, string description, float price, string fileNameInSystem, IFormFile fileName, int categoryIds, List<CategoryViewModel> categories)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            FileNameInSystem = fileNameInSystem;
            FileName = fileName;
            CategoryIds = categoryIds;
            Categories = categories;
        }

        //public AddViewModel(int id, string name, string description, float price, string fileNameInSystem, IFormFile fileName, List<Category> categories)
        //{
        //    Id = id;
        //    Name = name;
        //    Description = description;
        //    Price = price;
        //    FileNameInSystem = fileNameInSystem;
        //    FileName = fileName;
        //    Categories = categories;
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string FileNameInSystem { get; set; }
        public IFormFile FileName { get; set; }
        public int CategoryIds { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
