using System;
using System.Collections.Generic;

namespace FinalPoint.Services.Data
{
    public interface IUserRoleService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllUserRolesAsKeyValuePairs();
    }
}
