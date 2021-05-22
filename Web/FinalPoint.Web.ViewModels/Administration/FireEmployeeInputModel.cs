using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalPoint.Web.ViewModels.CustomAttributes;

namespace FinalPoint.Web.ViewModels.Administration
{
    public class FireEmployeeInputModel
    {

        public string ResultMessage { get; set; }

        [CustomRequired]
        [Display(Name = "Моля изберете служителя, който искате да уволните", Prompt = "Име на служител")]
        public int EmployeeToFire { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AvailableEmployeesToDelete { get; set; }
    }
}
