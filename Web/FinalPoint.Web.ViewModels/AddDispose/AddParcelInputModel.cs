using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalPoint.Web.ViewModels.CustomAttributes;

namespace FinalPoint.Web.ViewModels.AddDispose
{
    public class AddParcelInputModel
    {
        [CustomRequired]
        [Display(Name = "Описание", Prompt = "Описание на пратката")]
        public string Description { get; set; }

        [CustomRequired]
        [Display(Name = "Ширина")]
        public double Width { get; set; }

        [CustomRequired]
        [Display(Name = "Височина")]
        public double Height { get; set; }

        [CustomRequired]
        [Display(Name = "Дължина")]
        public double Length { get; set; }

        [CustomRequired]
        [Display(Name = "Тегло")]
        public double Weight { get; set; }

        [CustomRequired]
        [Display(Name = "Наложен платеж?")]
        public bool HasCashOnDelivery { get; set; }

        [Display(Name = "Цена за наложен платеж")]
        public double? CashOnDeliveryPrice { get; set; }

        public int SendingOfficeId { get; set; }

        [CustomRequired]
        [Display(Name = "Офис на получател", Prompt = "Въведете офиса на получателя")]
        public int ReceivingOfficeId { get; set; }

        [CustomRequired]
        [Display(Name = "Подател")]
        public int SenderId { get; set; }

        [CustomRequired]
        [Display(Name = "Получател")]
        public int RecipentId { get; set; }

        public int CurrentOfficeId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AllClients { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AllOffices { get; set; }
    }
}
