using System.Net.Mail;
using Eventy.Service.Domain.Emailing.Interfaces;
using Eventy.Service.Domain.User.Interfaces;

namespace Eventy.Service.Domain.Emailing.Services
{
    public class EmailingService : IEmailingService
    {
        private readonly string _smtpServerHost = "localhost";
        private readonly int _smtpServerPort = 2500;
        

        // public async Task<bool> Send(Guid senderId, List<Guid> recipientsIds, string subject, string body)
        // {
        //     try
        //     {

        //         List<Guid> participantsIds = new(recipientsIds);
        //         participantsIds.Append(senderId);

        //         var participants = await _userRepository.GetByIdAsync(participantsIds);

        //         SelectUser sender = participants?.FirstOrDefault(p => p.Id == senderId)!;
        //         List<SelectUser> recipients = participants?.Where(p => recipientsIds.Contains(p.Id))?.ToList()!;

        //         using (SmtpClient client = new SmtpClient(_smtpServerHost, _smtpServerPort))
        //         {

        //             using (MailMessage message = new MailMessage())
        //             {
        //                 message.From = new MailAddress(sender.Email);
        //                 message.Subject = subject;


        //                 // recipients.ForEach(recipient => message.To.Add(new MailAddress(recipient.Email)));

        //                 foreach (var recipient in recipients)
        //                 {
        //                     message.To.Add(new MailAddress(recipient.Email));

        //                     List<(string Key, string Value)> replacements = new()
        //                     {
        //                         (EmailConstants.USER_NAME, recipient.Name),
        //                         (EmailConstants.EVENT_NAME, "Event Name"),
        //                         (EmailConstants.EVENT_DATE, "Event Date"),
        //                         (EmailConstants.EVENT_LOCATION, "Event Location")
        //                     };

        //                     message.Body = MessageBody(body);

        //                     try
        //                     {
        //                         client.Send(message);
        //                         Console.WriteLine("Email sent successfully!");
        //                     }
        //                     catch (Exception ex)
        //                     {
        //                         Console.WriteLine($"Error sending email: {ex.Message}");
        //                         Console.WriteLine(ex);
        //                     }

        //                     message.To.Clear();
        //                 }

        //                 return true;

        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Error sending email: {ex.Message}");
        //         Console.WriteLine(ex);
        //         return false;
        //     }
        // }

        public bool Send(string senderEmail, string recipientEmail, string subject, string body)
        {
            using (SmtpClient client = new SmtpClient(_smtpServerHost, _smtpServerPort))
            {

                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(senderEmail);
                    message.To.Add(new MailAddress(recipientEmail));
                    message.Subject = subject;
                    message.Body = body;

                    try
                    {
                        client.Send(message);
                        Console.WriteLine("Email sent successfully!");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending email: {ex.Message}");
                        Console.WriteLine(ex);
                        return false;
                    }
                }
            }
        }
    }
}
