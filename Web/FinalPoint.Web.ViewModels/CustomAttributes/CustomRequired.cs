using System;
using System.ComponentModel.DataAnnotations;

namespace FinalPoint.Web.ViewModels.CustomAttributes
{
    public class CustomRequired : RequiredAttribute
    {
        public CustomRequired()
        {
            this.ErrorMessage = "Това поле е задължително.";
        }
    }
}
