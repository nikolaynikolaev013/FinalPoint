// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    public class AddDisposeController : Controller
    {
        // GET: /<controller>/
        public IActionResult AddParcel()
        {
            return this.View();
        }

        public IActionResult DisposeParcel()
        {
            return this.View();
        }
    }
}
