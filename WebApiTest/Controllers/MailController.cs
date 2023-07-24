using BusinessLayer.Concrete;
using EntityLayer.DTOs;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;


namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpPost("SendDiscount")]
        public ActionResult SendMail(MailDTO mail)
        {

            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential("shoplistmonovi@gmail.com", "your_application_password");

            //MailMessage mesaj = new MailMessage();
            //mesaj.To.Add("receiver_email@example.com");
            //mesaj.From = new MailAddress("shoplistmonovi@gmail.com", "Verify Account");
            //mesaj.IsBodyHtml = true;
            //mesaj.Subject = "Account Verification";
            //mesaj.Body = "Your account verification code: [YourVerificationCodeHere]";

            //try
            //{
            //    client.Send(mesaj);
            //    return Ok("Send Message Success");
            //}
            //catch (Exception ex)
            //{
            //    // Handle the exception or log the error
            //    return BadRequest("Failed to send the message: " + ex.Message);
            //}



            //  Sadece tek mail ile oluyor
            
            MimeMessage message = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("The Innovation Squad", "shoplistmonovi@gmail.com");

            message.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress(mail.Name, mail.SenderMail);
            message.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
             bodyBuilder.HtmlBody = "<table style='background-color:#fff;padding:10px;width:620px;text-align:left;border-top:10px solid #333333;" +
                    "border-bottom:10px solid #333333;border-left:10px solid #333333;border-right:10px solid #333333'" +
                    " width='630' cellspacing='0' cellpadding='0'><tbody>" +
                    "<tr><td>" +
                    "<table style='background-color:#ffffff;' width='100%' cellspacing='0' cellpadding = '0'>  <tbody><tr><td style = 'padding: 10px;'>" +
                    "<a href = 'https://monovi.com.tr/' target = '_blank'  > " +
                    "<img src = 'https://monovi.com.tr/img/logo/monovi-logo-grey.png' alt = '' width = '215' height = '55' border = '0' /></a></td>" +
                    "<td style = 'color: #1a2640; font-family: Arial; font-size: 13px;margin-left:50px;' align = 'right' > (0542) 245 71 68 " +
                    "<span style = 'color: #a5b9c5; font-size: 24px;' >|</span> " +
                    "<a style = 'text-decoration: none; color: #1a2640;' href = 'https://monovi.com.tr/' target = '_blank' data - saferedirecturl = '#'>" +
                    "https://monovi.com.tr/ </a> &nbsp; &nbsp; &nbsp;</td></tr>" +
                    "<tr><td colspan = '2' ><hr style='border: 1px dashed black;'/> </td></tr><tr><td style = 'padding: 10px; font-size: 12px; font-family: Arial;' colspan = '2' >" +
                    "<p style = 'margin: 0 0 10px 0;' > Gönderen: <strong>" + ".... Name......" + "</strong>,</p>" +
                    "<p style = 'margin: 0 0 10px 0;' > Email: <strong>" + ".....email....." + "</strong>,</p>" +
                    "<p style = 'margin: 0 0 10px 0;' > Konu: <strong>" + "....suject...." + "</strong>,</p>" +
                    "<p style = 'margin: 0 0 10px 0;' > Mesaj: <strong>" + " .....message...." + "</strong>,</p> <br /> <br />" +
                    "<p style = 'margin: 0 0 0 0;' >" +
                    "Her türlü sorunuzda bize " +
                    "<a style = 'color: #000000;' href = 'mailto:'" + " shoplistmonovi@gmail.com" + "target = '_blank'> " + " shoplistmonovi@gmail.com" + " </a> " +
                    "adresinden ulaşabilir veya " +
                    "<a href = 'tel:(542) 245 71 68' target = '_blank' > (0542) 245 71 68 </a> nolu telefondan IK Birimi ile görüşebilirsiniz.</p>" +
                    "<p style = 'margin: 20px 0 0 0;' > Saygılarımızla,</p><p style = 'margin: 5px 0 0 0;' > " +
                    "shoplistmonovi | Monovi</p></td></tr>" +
                    "<tr><td colspan = '2' ><hr style='border: 1px dashed black;'/></td></tr><tr><td style = 'padding: 10px; color: #808080; font-size: 12px;' colspan = '2'>" +
                    "<p style = 'margin: 0 0 0 0; font-family: Arial;' >" +
                    " Copyright &copy; 2023 shoplistmonovi | Tüm hakları saklıdır.</p></td></tr></tbody></table></td></tr></tbody></table> "; 
            
            message.Body = bodyBuilder.ToMessageBody();
            message.Subject = mail.Subject;
            
            
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("shoplistmonovi@gmail.com", "wgmqtjenewpschju");
            client.Send(message);
            client.Disconnect(true);
            return Ok(mail);
        }

        //[HttpPost]
        //public string GetCheckAccountCode(RegisterDTO register)
        //{
        //    Random r=new Random();
            
        //    MimeMessage message = new MimeMessage();

        //    MailboxAddress mailboxAddressFrom = new MailboxAddress("The Innovation Squad Create Account Code", "shoplistmonovi@gmail.com");

        //    message.From.Add(mailboxAddressFrom);

        //    MailboxAddress mailboxAddressTo = new MailboxAddress(register.Name, register.Email);
        //    message.To.Add(mailboxAddressTo);

        //    var bodyBuilder = new BodyBuilder();
        //    bodyBuilder.HtmlBody = "<table style='background-color:#fff;padding:10px;width:620px;text-align:left;border-top:10px solid #333333;" +
        //           "border-bottom:10px solid #333333;border-left:10px solid #333333;border-right:10px solid #333333'" +
        //           " width='630' cellspacing='0' cellpadding='0'><tbody>" +
        //           "<tr><td>" +
        //           "<table style='background-color:#ffffff;' width='100%' cellspacing='0' cellpadding = '0'>  <tbody><tr><td style = 'padding: 10px;'>" +
        //           "<a href = 'https://monovi.com.tr/' target = '_blank'  > " +
        //           "<img src = 'https://monovi.com.tr/img/logo/monovi-logo-grey.png' alt = '' width = '215' height = '55' border = '0' /></a></td>" +
        //           "<td style = 'color: #1a2640; font-family: Arial; font-size: 13px;margin-left:50px;' align = 'right' > (0542) 245 71 68 " +
        //           "<span style = 'color: #a5b9c5; font-size: 24px;' >|</span> " +
        //           "<a style = 'text-decoration: none; color: #1a2640;' href = 'https://monovi.com.tr/' target = '_blank' data - saferedirecturl = '#'>" +
        //           "https://monovi.com.tr/ </a> &nbsp; &nbsp; &nbsp;</td></tr>" +
        //           "<tr><td colspan = '2' ><hr style='border: 1px dashed black;'/> </td></tr><tr><td style = 'padding: 10px; font-size: 12px; font-family: Arial;' colspan = '2' >" +
        //           "<p style = 'margin: 0 0 10px 0;' > Gönderen: <strong>" + ".... Name......" + "</strong>,</p>" +
        //           "<p style = 'margin: 0 0 10px 0;' > Email: <strong>" + ".....email....." + "</strong>,</p>" +
        //           "<p style = 'margin: 0 0 10px 0;' > Konu: <strong>" + "....suject...." + "</strong>,</p>" +
        //           "<p style = 'margin: 0 0 10px 0;' > Mesaj: <strong>" + " .....message...." + "</strong>,</p> <br /> <br />" +
        //           "<p style = 'margin: 0 0 0 0;' >" +
        //           "Her türlü sorunuzda bize " +
        //           "<a style = 'color: #000000;' href = 'mailto:'" + " shoplistmonovi@gmail.com" + "target = '_blank'> " + " shoplistmonovi@gmail.com" + " </a> " +
        //           "adresinden ulaşabilir veya " +
        //           "<a href = 'tel:(542) 245 71 68' target = '_blank' > (0542) 245 71 68 </a> nolu telefondan IK Birimi ile görüşebilirsiniz.</p>" +
        //           "<p style = 'margin: 20px 0 0 0;' > Saygılarımızla,</p><p style = 'margin: 5px 0 0 0;' > " +
        //           "shoplistmonovi | Monovi</p></td></tr>" +
        //           "<tr><td colspan = '2' ><hr style='border: 1px dashed black;'/></td></tr><tr><td style = 'padding: 10px; color: #808080; font-size: 12px;' colspan = '2'>" +
        //           "<p style = 'margin: 0 0 0 0; font-family: Arial;' >" +
        //           " Copyright &copy; 2023 shoplistmonovi | Tüm hakları saklıdır.</p></td></tr></tbody></table></td></tr></tbody></table> ";

        //    message.Body = bodyBuilder.ToMessageBody();
        //    //message.Subject = mail.Subject;


        //    SmtpClient client = new SmtpClient();
        //    client.Connect("smtp.gmail.com", 587, false);
        //    client.Authenticate("shoplistmonovi@gmail.com", "wgmqtjenewpschju");
        //    client.Send(message);
        //    client.Disconnect(true);

        //    return "code";
        //}



    }
}
