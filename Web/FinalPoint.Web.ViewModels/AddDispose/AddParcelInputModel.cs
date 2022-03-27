namespace FinalPoint.Web.ViewModels.AddDispose
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Services.Mapping;
    using FinalPoint.Web.ViewModels.CustomAttributes;

    public class AddParcelInputModel : IMapFrom<AddParcelInputModel>, IMapTo<Parcel>, IHaveCustomMappings
    {
        public AddParcelInputModel()
        {
            this.AllOffices = new HashSet<KeyValuePair<string, string>>();
            this.SenderInputModel = new AddClientInputModel(ClientType.Подател);
            this.RecipentInputModel = new AddClientInputModel(ClientType.Получател);
            this.Weight = 1;
            this.Width = 0.20;
            this.Height = 0.10;
            this.Length = 0.20;
            this.NumberOfParts = 1;
        }

        [CustomRequired]
        [Display(Name = "Описание", Prompt = "Описание на пратката")]
        public string Description { get; set; }

        [CustomRequired]
        [Display(Name = "Ширина")]
        [Range(0, 30)]
        public double Width { get; set; }

        [CustomRequired]
        [Display(Name = "Височина")]
        [Range(0, 30)]
        public double Height { get; set; }

        [CustomRequired]
        [Display(Name = "Дължина")]
        [Range(0, 30)]
        public double Length { get; set; }

        [CustomRequired]
        [Display(Name = "Тегло")]
        [Range(0, int.MaxValue)]
        public double Weight { get; set; }

        [CustomRequired]
        [Display(Name = "Части")]
        [Range(0, int.MaxValue)]
        public int NumberOfParts { get; set; }

        [CustomRequired]
        [Display(Name = "Наложен платеж ")]
        public bool HasCashOnDelivery { get; set; }

        [Display(Name = "Цена за наложен платеж:")]
        public decimal CashOnDeliveryPrice { get; set; }

        [CustomRequired]
        [Display(Name = "Чупливо ")]
        public bool IsFragile { get; set; }

        [CustomRequired]
        [Display(Name = "Не палетизирай ")]
        public bool DontPaletize { get; set; }

        public ParcelChargeType ChargeType { get; set; }

        public decimal DeliveryPrice { get; set; }

        public ApplicationUser SendingEmployee { get; set; }

        public Office SendingOffice { get; set; }

        [Display(Name = "Офис на получател", Prompt = "Въведете офиса на получателя")]
        public int ReceivingOfficeId { get; set; }

        public AddClientInputModel SenderInputModel { get; set; }

        public AddClientInputModel RecipentInputModel { get; set; }

        public Office CurrentOffice { get; set; }

        public string CurrOfficeAsString { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AllOffices { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AddParcelInputModel, Parcel>()
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Description))
                .ForMember(x => x.Width, x => x.MapFrom(y => y.Width))
                .ForMember(x => x.Height, x => x.MapFrom(y => y.Height))
                .ForMember(x => x.Length, x => x.MapFrom(y => y.Length))
                .ForMember(x => x.Weight, x => x.MapFrom(y => y.Weight))
                .ForMember(x => x.NumberOfParts, x => x.MapFrom(y => y.NumberOfParts))
                .ForMember(x => x.HasCashOnDelivery, x => x.MapFrom(y => y.HasCashOnDelivery))
                .ForMember(x => x.CashOnDeliveryPrice, x => x.MapFrom(y => y.CashOnDeliveryPrice))
                .ForMember(x => x.IsFragile, x => x.MapFrom(y => y.IsFragile))
                .ForMember(x => x.DontPaletize, x => x.MapFrom(y => y.DontPaletize))
                .ForMember(x => x.SendingEmployee, x => x.MapFrom(y => y.SendingEmployee))
                .ForMember(x => x.CurrentOffice, x => x.MapFrom(y => y.CurrentOffice))
                .ForMember(x => x.SendingOffice, x => x.MapFrom(y => y.SendingOffice))
                .ForMember(x => x.ReceivingOfficeId, x => x.MapFrom(y => y.ReceivingOfficeId))
                .ForMember(x => x.ChargeType, x => x.MapFrom(y => y.ChargeType))
                .ForMember(x => x.DeliveryPrice, x => x.MapFrom(y => y.DeliveryPrice))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}