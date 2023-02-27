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
        public async Task<IActionResult> Index(int? id)
        {
            const int NumProductsToDisplayPerPage = 3; // # of Products we want displayed per page
            const int PageOffset = 1; // Account for page, so the current page's Products are not skipped

            // Set the current page counter to ID, unless it's null,
            // in which case set the current page to 1
            int currPage = id ?? 1;
            
            // Get the # of the last page needed to display all Products
            // in the db on the Products catalog page
            int lastPage = await GetLastPage(NumProductsToDisplayPerPage);

            // Get set number of Products from db, accounting for the current page number
            List<Product> allProducts = await dbContext.Products
                                                       .Skip(NumProductsToDisplayPerPage * (currPage - PageOffset)) // Skip the given # of Products
                                                       .Take(NumProductsToDisplayPerPage).ToListAsync(); // Get the given # of Products

            // Display them on the page
            return View(allProducts);
        }

        /// <summary>
        /// Gets count of all Products in db, and using the given number of Products per page, 
        /// <br></br>
        /// calculates the max number of pages necessary to display all Products
        /// </summary>
        /// <param name="NumProductsToDisplayPerPage">The number of Products to be displayed per page</param>
        /// <returns>The max number of pages necessary to display all Products in the db</returns>
        private async Task<int> GetLastPage(int NumProductsToDisplayPerPage)
        {
            // Get the total # of Products in the db
            int totalNumProducts = await dbContext.Products.CountAsync();

            // Calculate the maximum number of pages necessary to display all Products,
            // rounded up to account for half a page of products: ex. 3.5 pages would become 4
            double maxNumPages = Math.Ceiling((double)totalNumProducts / NumProductsToDisplayPerPage);

            // Return the last possible page based on prior calculation
            return Convert.ToInt32(maxNumPages);
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
            if (ModelState.IsValid)
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
            Product? currProduct = await dbContext.Products.FindAsync(productID);

            // If the specified product is null
            if (currProduct == null)
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

                // Prepare success message
                TempData["Message"] = $"{currProduct.ProductName} was updated successfully";

                // Send them back to the Product catalog
                return RedirectToAction("Index");
            }

            // If all Product data not valid
            return View(currProduct);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productID)
        {
            // Get the specified Product from the DB using it's ID
            Product? currProduct = await dbContext.Products.FindAsync(productID);

            // If the specified product is null
            if (currProduct == null)
            {
                // Display 404 error
                return NotFound();
            }

            // Otherwise display Product information
            return View(currProduct);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productID)
        {
            // Get the specified Product from the DB using it's ID
            Product? currProduct = await dbContext.Products.FindAsync(productID);

            // If the specified product is not null
            if (currProduct != null)
            {
                // Prepare DELETE Statement
                dbContext.Products.Remove(currProduct);

                // Execute query asynchronously
                await dbContext.SaveChangesAsync();

                // Prepare success message
                TempData["Message"] = $"{currProduct.ProductName} was deleted successfully";

                // Send them back to the Product catalog
                return RedirectToAction("Index");
            }

            // Otherwise, prepare error message
            TempData["Message"] = "This product has already been deleted";

            // Send them back to the Product catalog
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productID)
        {
            // Get the specified Product from the DB using it's ID
            Product? currProduct = await dbContext.Products.FindAsync(productID);

            // If the specified Product is null
            if (currProduct == null)
            {
                // Display 404 error
                return NotFound();
            }

            // Otherwise display Product information
            return View(currProduct);
        }
    }
}
