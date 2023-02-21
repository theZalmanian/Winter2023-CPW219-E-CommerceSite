using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSite.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
