using Microsoft.AspNetCore.Mvc;

namespace SkillProfiApi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
