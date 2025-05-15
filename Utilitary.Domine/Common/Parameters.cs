namespace Utilitary.Domine.Common
{
    using System.Data;

    public class Parameters
    {
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
        public DbType Type { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
