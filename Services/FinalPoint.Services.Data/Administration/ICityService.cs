namespace FinalPoint.Services.Data.Administration
{
    using System.Threading.Tasks;

    using FinalPoint.Web.ViewModels.Administration;

    public interface ICityService
    {
        Task<int> CreateNewCity(AddCityInputModel model);
    }
}
