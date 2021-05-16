// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
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

        // GET: /<controller>/
        public IActionResult AddParcel()
        {
            AddParcelInputModel model = new AddParcelInputModel();
            model.AllClients = this.clientService.GetAllClientsAsKeyValuePairs();
            model.AllOffices = this.officeService.GeAllOfficesAsKeyValuePairs();
            return this.View(model);
        }

        public IActionResult DisposeParcel()
        {
            return this.View();
        }
    }
}
