using E_CommerceSite.Data;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel currLogin)
        {
            // If all given data is valid
            if (ModelState.IsValid)
            {
                // Check database for given credentials (Email and Password)
                Member? currMember = await dbContext.Members.FirstOrDefaultAsync(currMember => 
                                                                                 currMember.MemberEmail == currLogin.Email && 
                                                                                 currMember.MemberPassword == currLogin.Password);

                // If the user exists, and the password was correct
                if (currMember != null)
                {
                    // Store the user's email for the current session
                    HttpContext.Session.SetString("Email", currMember.MemberEmail);

                    // Redirect the user to the home page
                    return RedirectToAction("Index", "Home");
                }

                // If the user doesn't exist or the password was incorrect, display error
                ModelState.AddModelError(string.Empty, "That email or password is incorrect!");
            }

            // If all data not valid, or credentials incorrect
            return View(currLogin);
        }
    }
}
