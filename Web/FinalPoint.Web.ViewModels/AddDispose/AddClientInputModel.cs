namespace FinalPoint.Web.ViewModels.AddDispose
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.CustomAttributes;

    public class AddClientInputModel
    {
        public AddClientInputModel(ClientType type)
        {
            this.AllClients = new HashSet<KeyValuePair<string, string>>();
            this.Type = type;
        }

        public ClientType Type { get; set; }

        public int ClientId { get; set; }

        [Display(Name = "Малко име", Prompt = "Въведете малкото име на клиента")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилно име", Prompt = "Въведете фамилното име на клиента")]
        public string LastName { get; set; }

        [Display(Name = "Телефонен номер", Prompt = "Въведете телефонния номер на клиента")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Адрес", Prompt = "Въведете адреса на клиента")]
        public string Address { get; set; }

        [Display(Name = "Имейл", Prompt = "Въведете имейла на клиента")]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AllClients { get; set; }
    }
}
