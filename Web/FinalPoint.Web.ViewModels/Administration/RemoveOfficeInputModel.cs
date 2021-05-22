namespace FinalPoint.Web.ViewModels.Administration
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FinalPoint.Web.ViewModels.CustomAttributes;

    public class RemoveOfficeInputModel
    {
        public string ResultMessage { get; set; }

        [CustomRequired]
        [Display(Name = "Моля изберете офиса, който искате да прекратите", Prompt = "Име на офис")]
        public int OfficeToRemove { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AvailableOfficesToRemove { get; set; }
    }
}
