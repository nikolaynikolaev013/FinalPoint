namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;

    using FinalPoint.Services.Data;
    using FinalPoint.Services.Data.Protocol;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/{protocolId}")]
    [ApiController]
    public class ProtocolController : Controller
    {
        private readonly IProtocolService protocolService;

        public ProtocolController(
            IProtocolService protocolService)
        {
            this.protocolService = protocolService;
        }

        [HttpGet]
        public int GetNumberOfCheckedParcels(int protocolId)
        {
            return this.protocolService
                    .GetNumberOfCheckedAndAddedParcels(protocolId);
        }

        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<bool> CloseProtocol(int protocolId)
        {
            return await this.protocolService.CloseProtocol(protocolId, null);
        }
    }
}
