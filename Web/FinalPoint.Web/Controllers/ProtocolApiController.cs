namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;
    using FinalPoint.Web.Business.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/{protocolId}")]
    [ApiController]
    public class ProtocolApiController : BaseController
    {
        private readonly IProtocolService protocolService;

        public ProtocolApiController(
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
            return await this.protocolService.CloseProtocolAsync(protocolId);
        }
    }
}
