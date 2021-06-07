using System;
using System.Collections.Generic;

namespace FinalPoint.Web.ViewModels.Home
{
    public class LoginUsersAndOfficesShowViewModel
    {
        public LoginUsersAndOfficesShowViewModel()
        {
            this.Users = new HashSet<string>();
            this.Offices = new HashSet<string>();
        }

        public ICollection<string> Users { get; set; }

        public ICollection<string> Offices { get; set; }
    }
}
