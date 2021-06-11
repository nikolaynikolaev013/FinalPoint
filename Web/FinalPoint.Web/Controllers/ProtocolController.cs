namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;

    using FinalPoint.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/{protocolId}")]
    [ApiController]
    public class ProtocolController : Controller
    {
        private readonly IProtocolService protocolParcelService;
        private readonly IProtocolService protocolService;

        public ProtocolController(
            IProtocolService protocolParcelService,
            IProtocolService protocolService)
        {
            this.protocolParcelService = protocolParcelService;
            this.protocolService = protocolService;
        }

        [HttpGet]
        public int GetNumberOfCheckedParcels(int protocolId)
        {
            return this.protocolParcelService
                    .GetNumberOfCheckedAndAddedParcels(protocolId);
        }

        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<bool> CloseProtocol(int protocolId)
        {
            return await this.protocolService.CloseProtocol(protocolId);
        }
    }
}
