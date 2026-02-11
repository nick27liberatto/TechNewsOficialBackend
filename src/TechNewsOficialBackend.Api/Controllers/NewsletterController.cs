namespace TechNewsOficialBackend.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class NewsletterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
