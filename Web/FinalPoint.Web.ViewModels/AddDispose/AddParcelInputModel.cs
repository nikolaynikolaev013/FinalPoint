namespace FinalPoint.Web.ViewModels.AddDispose
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.CustomAttributes;

    public class AddParcelInputModel
    {
        public AddParcelInputModel()
        {
            this.AllOffices = new HashSet<KeyValuePair<string, string>>();
            this.SenderInputModel = new AddClientInputModel(Data.Models.Enums.ClientType.Подател);
            this.RecipentInputModel = new AddClientInputModel(Data.Models.Enums.ClientType.Получател);
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
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [CustomRequired]
        [Display(Name = "Части")]
        [Range(0, double.MaxValue)]
        public int NumberOfParts { get; set; }

        [CustomRequired]
        [Display(Name = "Наложен платеж ")]
        public bool HasCashOnDelivery { get; set; }

        [Display(Name = "Цена за наложен платеж:")]
        public double? CashOnDeliveryPrice { get; set; }

        [CustomRequired]
        [Display(Name = "Чупливо ")]
        public bool IsFragile { get; set; }

        [CustomRequired]
        [Display(Name = "Не палетизирай ")]
        public bool DontPaletize { get; set; }

        public ParcelChargeType ChargeType { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int SendingEmployeeId { get; set; }

        public int SendingOfficeId { get; set; }

        [Display(Name = "Офис на получател", Prompt = "Въведете офиса на получателя")]
        public int ReceivingOfficeId { get; set; }

        public AddClientInputModel SenderInputModel { get; set; }

        public AddClientInputModel RecipentInputModel { get; set; }

        public int CurrentOfficeId { get; set; }

        public string CurrOfficeAsString { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AllOffices { get; set; }
    }
}
