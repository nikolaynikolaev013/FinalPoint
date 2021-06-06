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
        private readonly IProtocolService protocolService;

        public CheckParcelController(
                    IParcelService parcelService,
                    IUserService userService,
                    IProtocolService protocolService)
        {
            this.parcelService = parcelService;
            this.userService = userService;
            this.protocolService = protocolService;
        }

        [HttpGet]
        [Route("/[controller]/{parcelId}/{firstName}/{lastName}/{phoneNumber}/{isDispose}")]
        public async Task<IActionResult> SearchForParcel(int? parcelId, string firstName, string lastName, string phoneNumber, bool isDispose)
        {
            var user = this.userService.GetUserByClaimsPrincipal(this.User);

            var model = new TrackParcelResultModel();
            model.Parcels = this.parcelService.SearchForParcels(parcelId, firstName, lastName, phoneNumber, this.User, isDispose);

            foreach (var parcel in model.Parcels)
            {
                parcel.Protocols = this.protocolService.GetAllParcelProtocolsByParcelId(parcel.Id);
            }

            model.IsDispose = isDispose;
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
