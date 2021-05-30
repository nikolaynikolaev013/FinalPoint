using System;
using FinalPoint.Data.Models;
using FinalPoint.Data.Models.Enums;

namespace FinalPoint.Web.ViewModels.DTOs
{
    public class NewOrOpenProtocolViewModel
    {
        public NewOrOpenProtocolViewModel()
        {
        }

        public ProtocolType Type { get; set; }

        public string TranslatedType { get; set; }

        public string UserId { get; set; }

        public int RecipentOfficeId { get; set; }

        public string Message { get; set; }

        public string TypeOfMessage { get; set; }

        public Protocol Protocol { get; set; }
    }
}
