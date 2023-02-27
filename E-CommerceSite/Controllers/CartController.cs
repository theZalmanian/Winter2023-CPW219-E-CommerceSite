using E_CommerceSite.Data;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext dbContext;

        public CartController(ProductContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int productID)
        {
            // Get the specified Product from the DB using it's ID
            Product? currProduct = await dbContext.Products.FindAsync(productID);

            // If the specified product is null
            if (currProduct == null)
            {
                // Prepare error message
                TempData["Message"] = "This product no longer exists";

                // Send the user back to the Product catalog
                return RedirectToAction("Index", "Product");
            }

            // Prepare success message
            TempData["Message"] = $"{currProduct.ProductName} was added to the cart";

            // Send them back to the Product catalog
            return RedirectToAction("Index", "Product");
        }
    }
}
