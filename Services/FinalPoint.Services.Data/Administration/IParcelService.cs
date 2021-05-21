using System.Threading.Tasks;
using FinalPoint.Web.ViewModels.AddDispose;

namespace FinalPoint.Services.Data.Administration
{
    public interface IParcelService
    {
        public Task CreateAsync(AddParcelInputModel input);
    }
}
