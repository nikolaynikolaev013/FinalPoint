namespace FinalPoint.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserServices
    {
        IEnumerable<KeyValuePair<string, string>> GetAllUsersAsKeyValuePair();
    }
}
