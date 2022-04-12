namespace FinalPoint.Web.Business.Interfaces
{
    using System.Collections.Generic;

    public interface IUserRoleService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllUserRolesAsKeyValuePairs();
    }
}
