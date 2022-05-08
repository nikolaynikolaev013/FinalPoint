// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.ViewModels.LoadUnload;
    using FinalPoint.Web.ViewModels.Shared;
    using FinalPoint.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class LoadUnloadController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly IProtocolService protocolService;
        private readonly IUserService userService;

        public LoadUnloadController(
            IOfficeService officeService,
            IProtocolService protocolService,
            IUserService userService)
        {
            this.officeService = officeService;
            this.protocolService = protocolService;
            this.userService = userService;
        }

        public IActionResult Load()
        {
            LoadUnloadIndexViewModel model = this.LoadLoadUnloadViewModel(ProtocolType.Loading);
            return this.View("LoadUnload", model);
        }

        [HttpPost]
        public async Task<IActionResult> Load(LoadUnloadIndexViewModel input)
        {
            LoadUnloadProtocolViewModel model = await this.LoadLoadUnoadProtocolViewModel(input, ProtocolType.Loading);

            return this.View("LoadUnloadProtocol", model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadProtocol(int id)
        {
            var protocolInput = await this.protocolService.LoadOldProtocol(new ViewModels.DTOs.NewProtocolCreateOrOpenDataInputDto()
            {
                User = this.User,
                Id = id,
            });
            var parcelsTableShowData = await this.FillUpParcelsTableShowData(protocolInput.Protocol.Id, true);

            var model = new LoadUnloadProtocolViewModel()
            {
                ParcelTableShowViewData = parcelsTableShowData,
                Type = protocolInput.Protocol.Type,
                Id = protocolInput.Protocol.Id,
                TranslatedType = protocolInput.TranslatedType,
                Line = protocolInput.Protocol.OfficeTo.PostCode,
                Date = protocolInput.Protocol.CreatedOn,
                ParcelInsertViewModel = new ParcelInsertPartialViewModel()
                {
                    ButtonText = "Добавяне",
                },
            };

            model.IsClosed = protocolInput.Protocol.IsClosed;

            return this.View("LoadUnloadProtocol", model);
        }

        public IActionResult Unload()
        {
            LoadUnloadIndexViewModel model = this.LoadLoadUnloadViewModel(ProtocolType.Unloading);

            return this.View("LoadUnload", model);
        }

        [HttpPost]
        public async Task<IActionResult> Unload(LoadUnloadIndexViewModel input)
        {
            LoadUnloadProtocolViewModel model = await this.LoadLoadUnoadProtocolViewModel(input, ProtocolType.Unloading);

            return this.View("LoadUnloadProtocol", model);
        }

        [HttpGet]
        [Route("/CheckedParcelResult/{parcelId}/{protocolId}/{isCheck}")]
        public async Task<IActionResult> CheckedParcelResult(int parcelId, int protocolId, bool isCheck)
        {
            var responsibleUserPersonalId = this.userService
                                .GetUserById(this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                                .PersonalId;

            CheckParcelResponseModel model = new CheckParcelResponseModel();

            if (isCheck)
            {
                model = await this.protocolService.TryAddParcelInProtocol(parcelId, protocolId, responsibleUserPersonalId);
            }
            else
            {
                model = await this.protocolService.TryRemoveParcelFromProtocol(parcelId, protocolId, responsibleUserPersonalId);
            }

            return this.PartialView("_CheckResponsePartialView", model);
        }

        [HttpGet]
        [Route("/ReloadParcelsTable/{protocolId}")]
        public async Task<IActionResult> ReloadParcelsTable(int protocolId)
        {
            var model = await this.FillUpParcelsTableShowData(protocolId, false);

            return this.PartialView("_ParcelsTableShowPartialView", model);
        }

        private async Task<ParcelsTableShowModel> FillUpParcelsTableShowData(int protocolId, bool withDisposed = false)
        {
            var model = new ParcelsTableShowModel();

            var protocol = this.protocolService.GetProtocolWithOfficesById(protocolId);
            var user = this.userService.GetUserByClaimsPrincipal(this.User);

            await this.protocolService.LoadNewProtocolParcels(user, protocol.Type, protocol.Id, protocol.OfficeFromId, protocol.OfficeToId, true);
            model.Protocol = this.protocolService.GetProtocolWithOfficesById(protocolId);
            model.Parcels = this.protocolService.GetAllProtocolParcels(protocolId, withDisposed);

            foreach (var parcel in model.Parcels)
            {
                parcel.Parcel.Protocols = this.protocolService.GetAllParcelProtocolsByParcelId(parcel.Parcel.Id);
            }

            return model;
        }

        private LoadUnloadIndexViewModel LoadLoadUnloadViewModel(ProtocolType protocolType)
        {
            LoadUnloadIndexViewModel model = new LoadUnloadIndexViewModel();
            model.Type = protocolType;
            var currUser = this.userService.GetUserByClaimsPrincipal(this.User);
            model.Lines = this.officeService.GetLoadUnloadOffices(currUser.WorkOffice);
            model.TranslatedType = this.protocolService.TranslateType(model.Type);
            return model;
        }

        private async Task<LoadUnloadProtocolViewModel> LoadLoadUnoadProtocolViewModel(LoadUnloadIndexViewModel input, ProtocolType protocolType)
        {
            var protocolInput = await this.protocolService.CheckOrCreateProtocol(new ViewModels.DTOs.NewProtocolCreateOrOpenDataInputDto()
            {
                Type = protocolType,
                User = this.User,
                RecipentOfficeId = input.LineToLoad,
            });

            var parcelsTableShowData = await this.FillUpParcelsTableShowData(protocolInput.Protocol.Id);

            var model = new LoadUnloadProtocolViewModel()
            {
                ParcelTableShowViewData = parcelsTableShowData,
                Type = protocolInput.Protocol.Type,
                Message = protocolInput.Message,
                TypeOfMessage = protocolInput.TypeOfMessage,
                TranslatedType = protocolInput.TranslatedType,
                Id = protocolInput.Protocol.Id,
                Line = protocolInput.Protocol.OfficeTo.PostCode,
                Date = protocolInput.Protocol.CreatedOn,
                ParcelInsertViewModel = new ParcelInsertPartialViewModel()
                {
                    ButtonText = "Добавяне",
                },
            };

            model.IsClosed = protocolInput.Protocol.IsClosed;
            return model;
        }
    }
}
