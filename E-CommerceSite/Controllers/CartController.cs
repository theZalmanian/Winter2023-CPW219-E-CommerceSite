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
        public IActionResult ViewCart()
        {
            // Get all Products currently stored in the shopping cart, if any
            List<ProductCartViewModel> productCart = GetShoppingCartData();

            // Display them on the page
            return View(productCart);
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

            // Get all products currently stored in the shopping cart, if any
            List<ProductCartViewModel> productCart = GetShoppingCartData();

            // Add the current Product to the shopping cart
            productCart.Add(productInCart);

            // Save the shopping cart to the session
            SaveShoppingCartData(productCart);

            // Prepare success message
            TempData["Message"] = $"{currProduct.ProductName} was added to the cart";

            // Send them back to the Product catalog
            return RedirectToAction("Index", "Product");
        }

        /// <summary>
        /// Takes in a version of the Products shopping cart, 
        /// converts it to a JSON, and saves it to the session
        /// </summary>
        /// <param name="productCart">A list containing all Products in the shopping cart</param>
        private void SaveShoppingCartData(List<ProductCartViewModel> productCart)
        {
            // Convert shopping cart to JSON 
            string cookieData = JsonConvert.SerializeObject(productCart);

            // Add the current Product to the shopping cart cookie
            HttpContext.Response.Cookies.Append(CartCookieKey, cookieData, new CookieOptions()
            {
                // Set it's expiration date to 3 months
                Expires = DateTimeOffset.Now.AddMonths(3)
            });
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

        public IActionResult Remove(int productID)
        {
            // Get all Products currently stored in the shopping cart
            List<ProductCartViewModel> productCart = GetShoppingCartData();

            // Get the Product needing removal from the shopping cart using its ID
            ProductCartViewModel? productToRemove = productCart.FirstOrDefault(product => product.ProductID == productID);

            // If the specified product is not null
            if (productToRemove != null)
            {
                // Remove the specified Product from the shopping cart
                productCart.Remove(productToRemove);

                // Save the cart with the Product removed
                SaveShoppingCartData(productCart);

                // Prepare success message
                TempData["Message"] = $"{productToRemove.ProductName} was removed from the cart";

                // Send them back to the shopping cart
                return RedirectToAction(nameof(ViewCart));
            }

            // Otherwise, prepare error message
            TempData["Message"] = "This product has already been removed from the cart";

            // Send them back to the shopping cart
            return RedirectToAction(nameof(ViewCart));
        }
    }
}
