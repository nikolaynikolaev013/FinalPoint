namespace FinalPoint.Web.Business.Services
{
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using FinalPoint.Common;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.DTOs.Email;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using RazorEngineCore;

    public class EmailService : IEmailService
    {
        private const string BaseEmailTemplatePath = "/wwwroot/EmailTemplates/index.html";

        private readonly MailSettings mailSettings;
        private readonly IParcelService parcelService;
        private readonly IClientService clientService;
        private readonly RazorEngine razorEngine;
        private readonly string buildDir;

        public EmailService(
            IOptions<MailSettings> mailSettings,
            IParcelService parcelService,
            IClientService clientService)
        {
            this.mailSettings = mailSettings.Value;
            this.parcelService = parcelService;
            this.clientService = clientService;
            this.razorEngine = new RazorEngine();
            this.buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public async Task SendEmailAsync(MailRequestDto mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(this.mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(this.mailSettings.Host, this.mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(this.mailSettings.Mail, this.mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task<bool> SendNewParcelEmailsAsync(int parcelId)
        {
            var parcel = this.parcelService.GetParcelById(parcelId);
            var parcelFinalPrice = parcel.CashOnDeliveryPrice + (double)parcel.DeliveryPrice;
            var priceMainText = string.Format(MailConstants.PriceMainText, parcel.DeliveryPrice, parcelFinalPrice);
            string templateRaw = File.ReadAllText(this.buildDir + BaseEmailTemplatePath);

            var subject = string.Empty;
            var body = string.Empty;

            subject = $"{string.Format(MailConstants.NewParcelSenderSubject, parcel.SendingOffice.Name)}";
            body = $"{string.Format(MailConstants.NewParcelSenderMainText, parcel.SendingOffice.Name, parcel.ReceivingOffice.Name)} </br></br>{priceMainText}";

            await this.SendParcelUpdateEmailToCustomerAsync(
                parcel.SenderId,
                subject,
                body,
                templateRaw);

            subject = $"{string.Format(MailConstants.NewParcelRecipentSubject, parcel.SendingOffice.Name)}";
            body = $"{string.Format(MailConstants.NewParcelRecipentMainText, parcel.SendingOffice.Name, parcel.ReceivingOffice.Name)} </br></br>{priceMainText}";

            await this.SendParcelUpdateEmailToCustomerAsync(
                parcel.RecipentId,
                subject,
                body,
                templateRaw);

            return true;
        }

        public async Task<bool> SendUpdateParcelEmailsAsync(int parcelId)
        {
            var parcel = this.parcelService.GetParcelById(parcelId);
            var parcelFinalPrice = parcel.CashOnDeliveryPrice + (double)parcel.DeliveryPrice;
            var priceMainText = string.Format(MailConstants.PriceMainText, parcel.DeliveryPrice, parcelFinalPrice);
            string templateRaw = File.ReadAllText(this.buildDir + BaseEmailTemplatePath);

            var subject = string.Empty;
            var body = string.Empty;

            subject = $"{MailConstants.UpdateParcelSenderSubject}";
            body = $"{string.Format(MailConstants.UpdateParcelSenderMainText, parcel.ReceivingOffice.Name)} </br></br>{priceMainText}";

            await this.SendParcelUpdateEmailToCustomerAsync(
                parcel.SenderId,
                subject,
                body,
                templateRaw);

            subject = $"{string.Format(MailConstants.UpdateParcelRecipentMainText, parcel.CurrentOffice.Name)}";
            body = $"{string.Format(MailConstants.UpdateParcelRecipentMainText, parcel.ReceivingOffice.Name)} </br></br>{priceMainText}";

            await this.SendParcelUpdateEmailToCustomerAsync(
                parcel.RecipentId,
                subject,
                body,
                templateRaw);

            return true;
        }

        public async Task<bool> SendDisposedParcelEmailsAsync(int parcelId)
        {
            var parcel = this.parcelService.GetParcelWithDeletedById(parcelId);
            string templateRaw = File.ReadAllText(this.buildDir + BaseEmailTemplatePath);

            var subjectAndBody = string.Empty;

            subjectAndBody = MailConstants.DisposeParcelSenderMainText;

            await this.SendParcelUpdateEmailToCustomerAsync(
                parcel.SenderId,
                subjectAndBody,
                subjectAndBody,
                templateRaw);

            subjectAndBody = MailConstants.DisposeParcelRecipentMainText;

            await this.SendParcelUpdateEmailToCustomerAsync(
                parcel.RecipentId,
                subjectAndBody,
                subjectAndBody,
                templateRaw);

            return true;
        }

        private async Task<bool> SendParcelUpdateEmailToCustomerAsync(
            int clientId,
            string subject,
            string message,
            string templateRaw)
        {
            var compiledTemplate = this.razorEngine.Compile<RazorEngineTemplateBase<EmailClientParcelUpdateDto>>(templateRaw);

            var client = this.clientService
                    .GetClientById(clientId);

            if (client.EmailAddress == null)
            {
                return false;
            }

            var senderMailRequest = new MailRequestDto()
            {
                Subject = subject,
                ToEmail = client.EmailAddress,
                Body = compiledTemplate.Run(instance =>
                {
                    instance.Model = new EmailClientParcelUpdateDto()
                    {
                        Name = client.FirstName,
                        Message = message,
                    };
                }),
            };

            await this.SendEmailAsync(senderMailRequest);

            return true;
        }
    }
}
