using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    public class MainCategoryController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}