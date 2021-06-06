using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Services.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProtocolService protocolService;
        private readonly IUserService userService;

        public SearchController(IProtocolService protocolService,
            IUserService userService)
        {
            this.protocolService = protocolService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Parcel()
        {
            return View();
        }

        public IActionResult Protocol()
        {
            var user = this.userService.GetUserByClaimsPrincipal(this.User);
            var protocols = this.protocolService.GetLocalProtocolsByOfficeId(user.WorkOfficeId);
            return View();
        }
    }
}
