using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalPoint.Data.Models;
using FinalPoint.Web.ViewModels.CustomAttributes;

namespace FinalPoint.Web.ViewModels.Home
{
    public class HomeIndexInputModel
    {
        public string ResultMessage { get; set; }

        public string FullName { get; set; }

        public int CurrentWorkOfficeId { get; set; }

        public bool IsOwner { get; set; }

        public bool ShowOfficeModules { get; set; }

        public bool ShowAdministratorModule { get; set; }

        [CustomRequired]
        [Display(Name = "Моля изберете офиса, от името на който искате да работите.", Prompt = "Име на офис")]
        public int OfficeId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AvailableOffices { get; set; }
    }
}
