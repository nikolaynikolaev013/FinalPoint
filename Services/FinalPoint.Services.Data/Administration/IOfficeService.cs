namespace FinalPoint.Services.Data.Administration
{
    using System.Threading.Tasks;

    using FinalPoint.Web.ViewModels.Administration;

    public interface IOfficeService
    {
        Task CreateAsync(AddOfficeInputModel model);
    }
}
