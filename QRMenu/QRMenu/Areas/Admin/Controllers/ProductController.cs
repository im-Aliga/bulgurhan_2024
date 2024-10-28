using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRMenu.Areas.Admin.ViewModels;
using QRMenu.Areas.Client.ViewModels;
using QRMenu.Database;
using QRMenu.Database.Models;
using QRMenu.Services.Abstracts;

namespace QRMenu.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        //[HttpGet("~/")]
        [HttpGet("list", Name = "admin-product-list")]
        public async  Task<IActionResult> List()
        {
            var categories = await _dataContext.Categories.ToListAsync();
            var products = await _dataContext.Products
                .Select(p => new AddViewModel(
                    p.Id,p.Name, p.Description, p.Price,
                    _fileService.GetFileUrl(p.FileNameInSystem,Concreats.File.UploadDirectory.Product)))
                .ToListAsync();

            return View(products);
        }
        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel 
            {
                Categories = await _dataContext.Categories
                    .Select(c => new CategoryViewModel(c.Id, c.Name))
                    .ToListAsync(),
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddViewModel product)
        {
            try
            {
                if (product == null) { return View(new AddViewModel()); }

                var imageNameInSystem = await _fileService.UploadAsync(product.FileName, Concreats.File.UploadDirectory.Product);
                var newProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    FileName = product.FileName.FileName,
                    FileNameInSystem = imageNameInSystem,
                    CategoryId = product.CategoryIds,
                    CreatedAt = DateTime.Now
                };

                await _dataContext.AddAsync(newProduct);
                await _dataContext.SaveChangesAsync();

                return RedirectToRoute("admin-product-list");
            }
            catch (Exception ex)
            {
                return RedirectToRoute("admin-product-add");
            }

        
        }

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();



            return View(product);
        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> Update(int id, [FromForm] Product newFood)
        {
            var food = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (food == null) return NotFound();

            food.Name = newFood.Name;
            food.Price = newFood.Price;
            food.Description = newFood.Description;
            food.CreatedAt = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");
        }


        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (food == null) return NotFound();

            await _fileService.DeleteAsync(food.FileNameInSystem, Concreats.File.UploadDirectory.Product);
            _dataContext.Products.Remove(food);


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");
        }
    }
}
