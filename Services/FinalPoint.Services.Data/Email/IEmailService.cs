namespace FinalPoint.Services.Data.Mail
{
    using System.Threading.Tasks;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.Email;

    public interface IEmailService
    {
        Task SendEmailAsync(MailRequestDto mailRequest);

        Task<bool> SendNewParcelEmails(int parcelId);

        Task<bool> SendUpdateParcelEmails(int parcelId);

        Task<bool> SendDisposedParcelEmails(int parcelId);
    }
}
