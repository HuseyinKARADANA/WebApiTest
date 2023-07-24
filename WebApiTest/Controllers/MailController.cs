using EntityLayer.DTOs;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpPost]
        public ActionResult SendMail(MailDTO mail)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress(mail.Name,mail.SenderMail);
            
            message.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", "krdn35@gmail.com");
            message.To.Add(mailboxAddressTo);


            var bodyBuilder= new BodyBuilder();
            bodyBuilder.TextBody = mail.Body;
            message.Body=bodyBuilder.ToMessageBody();
            message.Subject=mail.Subject;

            SmtpClient client= new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(mail.SenderMail, "iybywapwkljbrgha");
            client.Send(message);
            client.Disconnect(true);
            return Ok("Send Message Success");
        }



    }
}
