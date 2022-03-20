using System;
using System.Collections.Generic;

namespace FinalPoint.Web.ViewModels.DTOs
{
    public class MailRequestDto
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
