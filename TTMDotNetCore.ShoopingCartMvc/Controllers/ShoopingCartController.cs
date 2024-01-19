using Microsoft.AspNetCore.Mvc;

namespace TTMDotNetCore.ShoopingCartMvc.Controllers
{
    public class ShoopingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
