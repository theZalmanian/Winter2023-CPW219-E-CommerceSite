using E_CommerceSite.Data;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext dbContext;

        public ProductController(ProductContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product currProduct)
        {
            // If all data is valid
            if(ModelState.IsValid)
            {
                // Prepare INSERT Statement
                dbContext.Products.Add(currProduct);

                // Execute query asynchronously
                await dbContext.SaveChangesAsync();

                // Output success message
                ViewData["Message"] = $"{currProduct.ProductName} was added successfully";
            }

            // If all Product data not valid
            return View(currProduct);
        }
    }
}
