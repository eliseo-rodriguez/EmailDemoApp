using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fromAddress = new MailAddress("milatispro@gmail.com", "LatisPro");
            var fromPassword = "*****314****";
            var toAddress = new MailAddress("eliseo_rodriguez@latisinformatica.com");

            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            }); ;

            StringBuilder template = new();
            template.AppendLine("Estimado, @Model.FirstName");
            template.AppendLine("<p>Gracias por comprar <b>@Model.ProductName</b>. Esperamos que lo disfrues.</p>");
            template.AppendLine("<br/>");
            template.AppendLine("Latis");


            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email.From(fromAddress.Address, fromAddress.DisplayName)
                                   .To("eliseo_rodriguez@latisinformatica.com")
                                   .Subject("Prueba Fluent Email")
                                   .UsingTemplate(template.ToString(), new { FirstName = "Eliseo", ProductName= "Latis/Pro"})
                                   //.Body("Gracias por usuar Fluen Email")
                                   .SendAsync();
        }
    }
}
