using System;
namespace FinalPoint.Web.ViewModels.DTOs
{
    public class ParcelCheckResultDto
    {
        public ParcelCheckResultDto()
        {
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public int NumberOfParts { get; set; }

        public string SendingOffice { get; set; }

        public string ReceivingOffice { get; set; }

    }
}