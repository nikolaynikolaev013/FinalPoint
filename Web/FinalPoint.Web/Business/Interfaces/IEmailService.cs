namespace FinalPoint.Web.Business.Interfaces
{
    using System.Threading.Tasks;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.Email;

    public interface IEmailService
    {
        Task SendEmailAsync(MailRequestDto mailRequest);

        Task<bool> SendNewParcelEmailsAsync(int parcelId);

        Task<bool> SendUpdateParcelEmailsAsync(int parcelId);

        Task<bool> SendDisposedParcelEmailsAsync(int parcelId);
    }
}
