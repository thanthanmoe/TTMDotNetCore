using Microsoft.AspNetCore.Mvc;

namespace TTMDotNetCore.WebMVCApp.Controllers
{
    public class UserController : Controller
    {
        [ActionName("Index")]
        public IActionResult UserIndex()
        {
            return View("UserIndex");
        }
    }
}
