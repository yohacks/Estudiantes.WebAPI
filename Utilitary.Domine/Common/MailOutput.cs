namespace Utilitary.Domine.Common
{
    using System;

    public class MailOutput
    {
        public int MailId { get; set; }

        public string MailFrom { get; set; }

        public string MailTo { get; set; }

        public string MailCc { get; set; }

        public string MailSubject { get; set; }

        public string MailBody { get; set; }

        public string MailResponse { get; set; }

        public int MailStatus { get; set; }

        public DateTime MailSend { get; set; }

        public string MailAttach { get; set; }
    }
}
