namespace Utilitary.Domine.Common
{
    using System;

    public class TemplateMail
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string NameTemplate { get; set; }
        public string CodTemplate { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public string FromTemplate { get; set; }
        public Boolean ReplayTemplate { get; set; }
    }
}
