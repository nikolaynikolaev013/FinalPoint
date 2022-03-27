using System;
using System.Collections.Generic;
using AutoMapper;
using FinalPoint.Data.Models;
using FinalPoint.Data.Models.Enums;
using FinalPoint.Services.Mapping;

namespace FinalPoint.Web.ViewModels.Shared
{
    public class SingleParcelSearchShowPartialViewModel : IMapFrom<Parcel>, IMapTo<SingleParcelSearchShowPartialViewModel>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Parcel, SingleParcelSearchShowPartialViewModel>()
                .ForMember(x => x.DateReceived, x => x.MapFrom(y => y.CreatedOn))
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Description))
                .ForMember(x => x.Width, x => x.MapFrom(y => y.Width))
                .ForMember(x => x.Height, x => x.MapFrom(y => y.Height))
                .ForMember(x => x.Length, x => x.MapFrom(y => y.Length))
                .ForMember(x => x.Weight, x => x.MapFrom(y => y.Weight))
                .ForMember(x => x.HasCashOnDelivery, x => x.MapFrom(y => y.HasCashOnDelivery))
                .ForMember(x => x.CashOnDeliveryPrice, x => x.MapFrom(y => y.CashOnDeliveryPrice))
                .ForMember(x => x.IsFragile, x => x.MapFrom(y => y.IsFragile))
                .ForMember(x => x.DontPaletize, x => x.MapFrom(y => y.DontPaletize))
                .ForMember(x => x.DeliveryPrice, x => x.MapFrom(y => y.DeliveryPrice))
                .ForMember(x => x.SendingEmployeeFullName, x => x.MapFrom(y => y.SendingEmployee.FullName))
                .ForMember(x => x.SendingOfficeCityName, x => x.MapFrom(y => y.SendingOffice.City.Name))
                .ForMember(x => x.SendingOfficeName, x => x.MapFrom(y => y.SendingOffice.Name))
                .ForMember(x => x.SendingOfficePostcode, x => x.MapFrom(y => y.SendingOffice.PostCode))
                .ForMember(x => x.SenderFullnameAndPhoneNumber, x => x.MapFrom(y => $"{y.Sender.FirstName} {y.Sender.LastName} - {y.Sender.PhoneNumber}"))
                .ForMember(x => x.ReceivingOfficeCityName, x => x.MapFrom(y => y.ReceivingOffice.City.Name))
                .ForMember(x => x.ReceivingOfficePostcode, x => x.MapFrom(y => y.ReceivingOffice.PostCode))
                .ForMember(x => x.ReceivingOfficeName, x => x.MapFrom(y => y.ReceivingOffice.Name))
                .ForMember(x => x.ReceivingOfficeId, x => x.MapFrom(y => y.ReceivingOffice.Id))
                .ForMember(x => x.RecipentFullnameAndPhoneNumber, x => x.MapFrom(y => $"{y.Recipent.FirstName} {y.Recipent.LastName} - {y.Recipent.PhoneNumber}"))
                .ForMember(x => x.CurrentOfficeId, x => x.MapFrom(y => y.CurrentOffice.Id))
                .ForMember(x => x.CurrentOfficeName, x => x.MapFrom(y => y.CurrentOffice.Name))
                .ForMember(x => x.CurrentOfficePostCode, x => x.MapFrom(y => y.CurrentOffice.PostCode))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}