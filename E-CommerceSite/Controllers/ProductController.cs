using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSite.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
