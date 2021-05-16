// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;
    using FinalPoint.Services.Data.Administration;
    using FinalPoint.Web.ViewModels.AddDispose;
    using Microsoft.AspNetCore.Mvc;

    public class AddDisposeController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly IClientService clientService;

        public AddDisposeController(IOfficeService officeService,
            IClientService clientService)
        {
            this.officeService = officeService;
            this.clientService = clientService;
        }

        public IActionResult AddParcel()
        {
            AddParcelInputModel model = new AddParcelInputModel();
            var allClients = this.clientService.GetAllClientsAsKeyValuePairs();

            model.SenderInputModel.AllClients = allClients;
            model.RecipentInputModel.AllClients = allClients;

            model.AllOffices = this.officeService.GeAllOfficesAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParcel(AddParcelInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var allClients = this.clientService.GetAllClientsAsKeyValuePairs();

                input.SenderInputModel.AllClients = allClients;
                input.RecipentInputModel.AllClients = allClients;

                input.AllOffices = this.officeService.GeAllOfficesAsKeyValuePairs();
                return this.View(input);
            }
            return this.View();
        }

        public IActionResult DisposeParcel()
        {
            return this.View();
        }
    }
}
