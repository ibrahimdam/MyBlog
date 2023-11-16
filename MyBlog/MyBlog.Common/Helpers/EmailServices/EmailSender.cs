using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Common.Helpers.EmailServices
{
    public class EmailSender : IEmailSender
    {
        // Bu değişkenler Mail göndermek için SMTP Server'ının ihtiyacı olan değişkenlerdir.
        //SMTP server nedir? : Mail gönderip almayı sağlayan sever.
        private string _host;
        private int _port;
        private string _username;
        private string _password;
        private bool _enableSSL;

        public EmailSender(string host, int port, bool enableSSL, string username, string password)
        {
            this._enableSSL = enableSSL;
            this._port = port;
            this._username = username;
            this._password  = password;
            this._host = host;
        }

        // email: To => kime gönderilecek
        // subject : konu
        // htmlMessage: mesaj içeriği
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = this._enableSSL
            };
            
            return client.SendMailAsync(
                new MailMessage(_username,email,subject,htmlMessage)
                {
                    IsBodyHtml = true
                });
        }
    }
}
