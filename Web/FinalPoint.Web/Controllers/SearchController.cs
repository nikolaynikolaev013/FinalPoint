namespace FinalPoint.Web.Controllers
{
    using FinalPoint.Web.Business.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly IProtocolService protocolService;
        private readonly IUserService userService;

        public SearchController(
            IProtocolService protocolService,
            IUserService userService)
        {
            this.protocolService = protocolService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Parcel()
        {
            return this.View();
        }

        public IActionResult Protocol()
        {
            var user = this.userService.GetUserByClaimsPrincipal(this.User);
            var protocols = this.protocolService.GetLocalProtocolsByOfficeId(user.WorkOfficeId);
            return this.View(protocols);
        }
    }
}
