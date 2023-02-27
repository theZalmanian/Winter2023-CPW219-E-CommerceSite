using E_CommerceSite.Data;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_CommerceSite.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// A connection to the database
        /// </summary>
        private readonly ProductContext dbContext;

        /// <summary>
        /// The key to access the shopping cart cookie
        /// </summary>
        private const string CartCookieKey = "ShoppingCart";

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

            // Create Product cart vm to use in adding a product to the cart
            ProductCartViewModel productInCart = new(currProduct.ProductID, 
                                                     currProduct.ProductName, 
                                                     currProduct.ProductPrice);

            // Create cart list to store all added Products
            List<ProductCartViewModel> productCart = GetShoppingCartData();
            
            // Add the current product to the cart
            productCart.Add(productInCart);

            // Convert cart to JSON 
            string cookieData = JsonConvert.SerializeObject(productCart);

            // 
            HttpContext.Response.Cookies.Append(CartCookieKey, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddMonths(3)
            });

            // Prepare success message
            TempData["Message"] = $"{currProduct.ProductName} was added to the cart";

            // Send them back to the Product catalog
            return RedirectToAction("Index", "Product");
        }

        /// <summary>
        /// Gets and returns the current list of Products in the users "shopping cart" cookie. 
        /// If the cookie does not exist, an empty list will be returned instead.
        /// </summary>
        /// <returns>A list containing all Products in cart if exists; otherwise and empty list</returns>
        private List<ProductCartViewModel> GetShoppingCartData()
        {
            // Get the cart cookie data from session
            string? cookieData = HttpContext.Request.Cookies[CartCookieKey];

            // If the given cookie data is null
            if(string.IsNullOrWhiteSpace(cookieData))
            {
                // Return an empty list
                return new List<ProductCartViewModel>();
            }

            // Otherwise, return a list containing all Products in shopping cart
            return JsonConvert.DeserializeObject<List<ProductCartViewModel>>(cookieData);
        }
    }
}
