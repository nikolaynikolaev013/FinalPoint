namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;
    using FinalPoint.Services.Data.Mail;
    using FinalPoint.Services.Data.Parcel;
    using FinalPoint.Services.Data.Protocol;
    using FinalPoint.Services.Data.User;
    using FinalPoint.Web.ViewModels.TrackParcel;
    using Microsoft.AspNetCore.Mvc;

    public class ParcelApiController : Controller
    {
        private readonly IParcelService parcelService;
        private readonly IUserService userService;
        private readonly IProtocolService protocolService;
        private readonly IMailService mailService;

        public ParcelApiController(
                    IParcelService parcelService,
                    IUserService userService,
                    IProtocolService protocolService,
                    IMailService mailService)
        {
            this.parcelService = parcelService;
            this.userService = userService;
            this.protocolService = protocolService;
            this.mailService = mailService;
        }

        [HttpGet]
        [Route("/[controller]/{parcelId}/{firstName}/{lastName}/{phoneNumber}/{isDispose}")]
        public IActionResult SearchForParcel(int? parcelId, string firstName, string lastName, string phoneNumber, bool isDispose)
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

            if (result)
            {
                result = await this.mailService.SendDisposedParcelEmails(parcelId);
            }

            return this.Ok(result);
        }
    }
}
