using Microsoft.AspNetCore.Mvc;

namespace MicroOrder.Controllers
{
    public class ConsumerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
