using E_CommerceSite.Data;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSite.Controllers
{
    public class MemberController : Controller
    {
        /// <summary>
        /// A connection to the database
        /// </summary>
        private readonly ProductContext dbContext;

        public MemberController(ProductContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel currRegistration)
        {
            // If all given data is valid
            if(ModelState.IsValid)
            {
                // Map RegisterViewModel data to Member object
                Member newMember = new()
                {
                    MemberEmail = currRegistration.Email,
                    MemberPassword = currRegistration.Password,
                };

                // Setup INSERT statement
                await dbContext.Members.AddAsync(newMember);

                // Execute query asynchronously
                await dbContext.SaveChangesAsync();

                // Redirect the user to the home page
                return RedirectToAction("Index", "Home");
            }

            // If all Registration data not valid
            return View(currRegistration);
        }
    }
}
