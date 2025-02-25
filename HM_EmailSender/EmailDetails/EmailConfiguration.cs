using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_EmailSender.EmailDetails
{
    public class EmailConfiguration
    {
        public string From { get; set; } = "nastyapivza1999@gmail.com";
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public string UserName { get; set; } = "nastyapivza1999@gmail.com";
        public string Password { get; set; } = "qnro ctir hgto eymb";

    }
}
