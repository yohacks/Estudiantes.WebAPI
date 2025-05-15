namespace Utilitary.Domine.Common
{
    using System;

    public class LogMiddleware
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Environment { get; set; }

        public string Method { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public string QueryString { get; set; }

        public string Request { get; set; }

        public string StatusCode { get; set; }

        public string Response { get; set; }

        public DateTime DateRegister { get; set; }        

    }

    public class LogResponse
    {
        public string StatusCode { get; set; }

        public string Response { get; set; }
    }
}
