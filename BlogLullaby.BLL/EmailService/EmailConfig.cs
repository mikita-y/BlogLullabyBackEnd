using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.BLL.EmailService
{
    public class EmailConfig
    {
        public string OrganizationEmailAdres { get; set; }
        public string OrganizationName { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }
}
