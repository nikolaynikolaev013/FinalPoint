namespace FinalPoint.Web.ViewModels.CustomAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PersonalId : RegularExpressionAttribute
    {
        private const string RegularExp = "[0-9]{4,}";

        public PersonalId()
            : base(RegularExp)
        {
            this.ErrorMessage = "Моля въведете валиден персонален код.";
        }
    }
}
