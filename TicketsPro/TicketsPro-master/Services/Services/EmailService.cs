using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace Services.Services
{
    public class EmailService
    {
        private readonly string _from;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSsl;
        public EmailService()
        {
            _from = ConfigurationManager.AppSettings["MailFrom"];
            _password = ConfigurationManager.AppSettings["MailPassword"];
            _host = ConfigurationManager.AppSettings["SmtpHost"];
            _port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            _enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"] ?? "true");
        }
        public void EnviarCodigo(string to, string asunto, string cuerpo)
        {
            using (var client = new SmtpClient(_host, _port))
            {
                client.Credentials = new NetworkCredential(_from, _password);
                client.EnableSsl = _enableSsl;
                var mail = new MailMessage(_from, to)
                {
                    Subject = asunto,
                    Body = cuerpo,
                    IsBodyHtml = false
                };
                client.Send(mail);
            }
        }
        public void EnviarEmail(string to, string asunto, string cuerpo, bool esHtml = true)
        {
            using (var client = new SmtpClient(_host, _port))
            {
                client.Credentials = new NetworkCredential(_from, _password);
                client.EnableSsl = _enableSsl;
                var mail = new MailMessage(_from, to)
                {
                    Subject = asunto,
                    Body = cuerpo,
                    IsBodyHtml = esHtml
                };
                client.Send(mail);
            }
        }
    }
}
