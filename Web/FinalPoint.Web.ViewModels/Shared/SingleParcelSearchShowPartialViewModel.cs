using System;
using System.Collections.Generic;
using FinalPoint.Data.Models;
using FinalPoint.Data.Models.Enums;

namespace FinalPoint.Web.ViewModels.Shared
{
    public class SingleParcelSearchShowPartialViewModel
    {
        public SingleParcelSearchShowPartialViewModel()
        {
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Weight { get; set; }

        public int NumberOfParts { get; set; }

        public bool HasCashOnDelivery { get; set; }

        public double? CashOnDeliveryPrice { get; set; }

        public bool IsFragile { get; set; }

        public bool DontPaletize { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string SendingEmployeeFullName { get; set; }

        public string SendingOfficeCityName { get; set; }

        public int SendingOfficePostcode { get; set; }

        public string SendingOfficeName { get; set; }

        public string SenderFullnameAndPhoneNumber { get; set; }

        public string ReceivingOfficeCityName { get; set; }

        public int ReceivingOfficePostcode { get; set; }

        public string ReceivingOfficeName { get; set; }

        public string RecipentFullnameAndPhoneNumber { get; set; }

        public int CurrentOfficeId { get; set; }

        public string CurrentOfficeCityName { get; set; }

        public string CurrentOfficeName { get; set; }

        public int CurrentOfficePostCode { get; set; }

        public int ReceivingOfficeId { get; set; }

        public DateTime DateReceived { get; set; }

        public ICollection<ProtocolParcel> Protocols { get; set; }
    }
}
