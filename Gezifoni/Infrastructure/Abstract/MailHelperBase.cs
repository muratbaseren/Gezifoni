using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gezifoni.Infrastructure.Concrete;

namespace Gezifoni.Infrastructure.Abstract
{
    internal abstract class MailHelperBase
    {
        public string MailHost { get; protected set; }
        public int MailPort { get; protected set; }
        public string MailUsername { get; protected set; }
        public string MailPassword { get; protected set; }

        protected MailHelperBase()
        {
            MailHost = ConfigHelper.MailHost;
            MailPort = int.Parse(ConfigHelper.MailPort);
            MailUsername = ConfigHelper.MailUid;
            MailPassword = ConfigHelper.MailPass;
        }

        protected MailHelperBase(string host, int port, string username, string password)
        {
            MailHost = host;
            MailPort = port;
            MailUsername = username;
            MailPassword = password;
        }

        public abstract bool SendMail(string body, string to, string subject, bool isHtml);
        public abstract bool SendMail(string body, List<string> to, string subject, bool isHtml);
    }
}
