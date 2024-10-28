using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRMenu.Areas.Admin.ViewModels;
using QRMenu.Database;

namespace QRMenu.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "Categorys")]
    public class Categorys : ViewComponent
    {

        private readonly DataContext _dataContext;

        public Categorys(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                await _dataContext.Categories
                .Select(c => new CategoryViewModel(c.Id, c.Name)).ToListAsync();


            return View(model);
        }
    }
}
