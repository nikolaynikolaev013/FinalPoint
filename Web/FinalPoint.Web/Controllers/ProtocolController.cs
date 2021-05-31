using System;
using FinalPoint.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalPoint.Web.Controllers
{
    [Route("[controller]/{protocolId}")]
    [ApiController]
    public class ProtocolController : Controller
    {
        private readonly IProtocolService protocolParcelService;

        public ProtocolController(IProtocolService protocolParcelService)
        {
            this.protocolParcelService = protocolParcelService;
        }

        [HttpGet]
        public int GetNumberOfCheckedParcels(int protocolId)
        {
            return this.protocolParcelService
                    .GetNumberOfCheckedAndAddedParcels(protocolId);
        }
    }
}
