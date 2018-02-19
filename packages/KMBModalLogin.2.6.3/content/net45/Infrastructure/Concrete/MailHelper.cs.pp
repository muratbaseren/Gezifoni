using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using $rootnamespace$.Infrastructure.Abstract;

namespace $rootnamespace$.Infrastructure.Concrete
{
    internal partial class MailHelper : MailHelperBase
    {
        public MailHelper() : base()
        {
        }

        public MailHelper(string host, int port, string username, string password) : base(host, port, username, password)
        {
        }



        public override bool SendMail(string body, string to, string subject, bool isHtml = true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }

        public override bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {
            bool result = false;

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(MailUsername);

                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var smtp =
                    new SmtpClient(MailHost, MailPort))
                {
                    smtp.EnableSsl = false;
                    smtp.Credentials =
                        new NetworkCredential(MailUsername, MailPassword);

                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception)
            {
                // TODO : Write log for mail sending exception.
            }

            return result;
        }
    }
}