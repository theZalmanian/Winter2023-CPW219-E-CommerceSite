using E_CommerceSite.Data;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            // Get all Products from the db
            List<Product> allProducts = await dbContext.Products.ToListAsync();

            // Display them on the page
            return View(allProducts);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int productID)
        {
            // Get the specified Product from the DB using it's ID
            Product currProduct = await dbContext.Products.FindAsync(productID);

            // If the specified product is null
            if(currProduct == null)
            {
                // Display 404 error
                return NotFound();
            }

            // Otherwise display Product information
            return View(currProduct);
        }

        [HttpPost] 
        public async Task<IActionResult> Edit(Product currProduct)
        {
            // If all data is valid
            if (ModelState.IsValid)
            {
                // Prepare UPDATE Statement
                dbContext.Products.Update(currProduct);

                // Execute query asynchronously
                await dbContext.SaveChangesAsync();

                // Output success message
                ViewData["Message"] = $"{currProduct.ProductName} was updated successfully";
            }

            // If all Product data not valid
            return View(currProduct);
        }
    }
}
