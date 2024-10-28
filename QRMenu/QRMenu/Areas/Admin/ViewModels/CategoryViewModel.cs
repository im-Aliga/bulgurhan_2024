namespace QRMenu.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public CategoryViewModel()
        {
            
        }
        public int?Id { get; set; }
        public string Name { get; set; }
    }
}
