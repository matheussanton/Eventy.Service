namespace Eventy.Service.Domain.Emailing.Interfaces
{
    public interface IEmailingService
    {
        public bool Send(string senderEmail, string recipientEmail, string subject, string body);
    }
}
