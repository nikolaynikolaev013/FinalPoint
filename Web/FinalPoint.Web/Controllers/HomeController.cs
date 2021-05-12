namespace FinalPoint.Web.Controllers
{
    using System.Diagnostics;
    using FinalPoint.Web.ViewModels;
    using FinalPoint.Web.ViewModels.GroupUngroup;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.FullName = "Nikolay Nikolaev";
            return this.View(model);
        }

        

        public IActionResult Group(string line)
        {
            GroupUngroupViewModel model = new GroupUngroupViewModel();

            return this.View("GroupUngroup", model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
