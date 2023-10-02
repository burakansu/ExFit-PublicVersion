
using Data.Entities.ExFit;
using System.Net;
using System.Net.Mail;
using Task = System.Threading.Tasks.Task;

namespace Business
{
    public class MailManager : ManagerBase
    {
        public async Task Send(string CompanyName, string MailText, Member Member = null, User User = null)
        {
            string frommail = "exfitgymmanager@gmail.com";
            string frompassword = "tjvyfibzabbctmnb";

            MailMessage Message = new MailMessage();
            Message.From = new MailAddress(frommail);
            Message.Subject = CompanyName;

            if (Member != null)
                Message.To.Add(Member.Mail);

            if (User != null)
                Message.To.Add(User.Mail);

            Message.Body =
                "<html>" +
                "<body>" +
                "<H1>"+ CompanyName +" - Otomatik Mesaj</H1>" +
                "<p>" + MailText + "</p>" +
                "<br><br>" +
                "</body>" +
                "</html>";

            Message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(frommail, frompassword),
                EnableSsl = true,
            };

            try
            {
                smtpClient.Send(Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}