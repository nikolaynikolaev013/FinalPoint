// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Services.Data;
    using FinalPoint.Web.ViewModels.DTOs.LoadUnload;
    using FinalPoint.Web.ViewModels.LoadUnload;
    using FinalPoint.Web.ViewModels.Shared;
    using FinalPoint.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        // GET: /<controller>/
        public IActionResult Load()
        {
            LoadUnloadIndexViewModel model = new LoadUnloadIndexViewModel();
            var currUser = this.userService.GetUserByClaimsPrincipal(this.User);
            model.Lines = this.officeService.GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(currUser.WorkOfficeId);
            model.Type = ProtocolType.Loading;
            return this.View("LoadUnload", model);
        }

        [HttpPost]
        public async Task<IActionResult> Load(LoadUnloadIndexViewModel input)
        {
            var protocolInput = await this.protocolService.CheckOrCreateProtocol(new ViewModels.DTOs.NewProtocolCreateOrOpenDataInputDto()
            {
                Type = ProtocolType.Loading,
                UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
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

            model.IsClosed = this.protocolService.IsClosed(model.Id);

            return this.View("LoadUnloadProtocol", model);
        }

        public IActionResult Unload()
        {
            LoadUnloadIndexViewModel model = new LoadUnloadIndexViewModel();
            model.Type = ProtocolType.Unloading;
            var currUser = this.userService.GetUserByClaimsPrincipal(this.User);
            model.Lines = this.officeService.GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(currUser.WorkOfficeId);

            return this.View("LoadUnload", model);
        }

        [HttpPost]
        public async Task<IActionResult> Unload(LoadUnloadIndexViewModel input)
        {
            var protocolInput = await this.protocolService.CheckOrCreateProtocol(new ViewModels.DTOs.NewProtocolCreateOrOpenDataInputDto()
            {
                Type = ProtocolType.Unloading,
                UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
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

            model.IsClosed = this.protocolService.IsClosed(model.Id);

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
            var model = await this.FillUpParcelsTableShowData(protocolId);

            return this.PartialView("_ParcelsTableShowPartialView", model);
        }

        private async Task<ParcelsTableShowModel> FillUpParcelsTableShowData(int protocolId)
        {
            var model = new ParcelsTableShowModel();

            var protocol = this.protocolService.GetProtocolWithOfficesById(protocolId);
            var user = this.userService.GetUserByClaimsPrincipal(this.User);

            await this.protocolService.LoadNewProtocolParcels(user, protocol.Type, protocol.Id, protocol.OfficeFromId, protocol.OfficeToId);
            model.Protocol = this.protocolService.GetProtocolWithOfficesById(protocolId);
            model.Parcels = this.protocolService.GetAllProtocolParcels(protocolId);
            return model;
        }
    }
}
