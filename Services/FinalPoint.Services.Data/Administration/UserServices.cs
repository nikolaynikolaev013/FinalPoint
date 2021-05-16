using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Data.Common.Repositories;
using FinalPoint.Data.Models;

namespace FinalPoint.Services.Data.Administration
{
    public class UserServices : IUserServices
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRep;

        public UserServices(IDeletableEntityRepository<ApplicationUser> usersRep)
        {
            this.usersRep = usersRep;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllUsersAsKeyValuePair()
        {
            return this.usersRep
                .All()
                .OrderBy(x=>x.PersonalId)
                .Select(x => new
                {
                    x.FullName,
                    x.PersonalId,
                }).ToList()
                .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString()));
        }
    }
}
