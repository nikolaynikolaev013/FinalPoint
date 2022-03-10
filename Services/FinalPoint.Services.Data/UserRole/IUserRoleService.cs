namespace FinalPoint.Services.Data.UserRole
{
    using System.Collections.Generic;

    public interface IUserRoleService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllUserRolesAsKeyValuePairs();
    }
}
