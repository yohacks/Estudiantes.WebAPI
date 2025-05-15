namespace Utilitary.Domine.Common
{
    public class Transaction
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Details Details { get; set; }
    }
    public class Details
    {
        public string Output { get; set; }
        public string OutputType { get; set; }
        public string Id { get; set; }
    }
}
