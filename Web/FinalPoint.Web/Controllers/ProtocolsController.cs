namespace FinalPoint.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Services.Data;
    using FinalPoint.Web.ViewModels.DTOs;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ProtocolsController : BaseController
    {
        private readonly IProtocolService protocolService;

        public ProtocolsController(IProtocolService protocolService)
        {
            this.protocolService = protocolService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult> Post(NewProtocolCreateOrOpenDataInputDto input)
        {
            input.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = await this.protocolService.CheckOrCreateProtocol(input);
            return this.Json(response);
        }
    }
}
