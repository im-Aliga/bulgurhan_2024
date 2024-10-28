using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRMenu.Areas.Admin.ViewModels;
using QRMenu.Areas.Client.ViewModels;
using QRMenu.Database;
using QRMenu.Services.Abstracts;

namespace QRMenu.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private DataContext _dataContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        [HttpGet("~/")]
        [HttpGet("index", Name = "client-home-index")]
        public async Task<IActionResult> Index()
        
        {
            var products= await _dataContext.Products.Include(c=> c.Category).Select(p=> new ProductViewModel(p.Name,p.Description,p.Price,_fileService.GetFileUrl(p.FileNameInSystem,Concreats.File.UploadDirectory.Product)
                ,_dataContext.Categories.Where(c=> c.Id == p.Category.Id).Select(c=> new CategoryViewModel(c.Id,c.Name)).ToList()))
                .ToListAsync();

            return View(products);
        }

   
    }
}
