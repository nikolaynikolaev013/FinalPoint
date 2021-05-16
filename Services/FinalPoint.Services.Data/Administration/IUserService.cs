namespace FinalPoint.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllUsersAsKeyValuePair();

        Task<IEnumerable<int>> GetAllPersonalIds();
    }
}
