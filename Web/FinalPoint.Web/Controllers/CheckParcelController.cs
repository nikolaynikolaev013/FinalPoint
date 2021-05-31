using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalPoint.Services.Data;
using FinalPoint.Web.ViewModels.TrackParcel;
using Microsoft.AspNetCore.Mvc;

namespace FinalPoint.Web.Controllers
{
    public class CheckParcelController : Controller
    {
        private readonly IParcelService parcelService;
        private readonly IUserService userService;

        public CheckParcelController(
                    IParcelService parcelService,
                    IUserService userService)
        {
            this.parcelService = parcelService;
            this.userService = userService;
        }

        [HttpGet]
        [Route("/[controller]/{parcelId}/{firstName}/{lastName}/{phoneNumber}")]
        public async Task<IActionResult> SearchForParcel(int? parcelId, string firstName, string lastName, string phoneNumber)
        {
            var user = this.userService.GetUserByClaimsPrincipal(this.User);

            var model = new TrackParcelResultModel();
            model.Parcels = this.parcelService.SearchForParcels(parcelId, firstName, lastName, phoneNumber, this.User);
            model.CurrUserWorkOfficeId = user.WorkOfficeId;

            return this.PartialView("_ParcelSearchShowPartialView", model);
        }

        [HttpDelete]
        [IgnoreAntiforgeryToken]
        [Route("/[controller]/{parcelId}")]
        public async Task<IActionResult> DisposeParcel(int parcelId)
        {
            var result = await this.parcelService.DisposeParcel(parcelId, this.User);

            return this.Ok(result);
        }
    }
}
