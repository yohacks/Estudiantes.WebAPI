namespace Utilitary.Core.Common.Utilities
{
    using Newtonsoft.Json;
    using SpreadsheetLight;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Domine;
    using Utilitary.Domine.Common;

    public class InternalServices
    {
        private readonly IProceduresDataEstudiantes _proceduresDataEstudiantes;

        public InternalServices(IProceduresDataEstudiantes proceduresDataUnickChile)
        {
            _proceduresDataEstudiantes = proceduresDataUnickChile;
        }

        public void SaveLogging(LogMiddleware InfLogging)
        {
            try
            {
                List<Parameters> ParametrosLog = new List<Parameters>
                {
                    new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = 1 },
                    new Parameters { ParameterName = "@RefLogging", Type = DbType.String, ParameterValue = JsonConvert.SerializeObject(InfLogging).ToString() }
                };

                _proceduresDataEstudiantes.ExecuteSpSingleThread("dbo.PRC_SYS_LogError", ParametrosLog);
            }
            catch (Exception ex)
            {
                this.SaveErrorLog(nameof(SaveLogging), ex);
            }

            return;
        }

        public void SaveErrorLog(string refError, Exception ex, string AditionalInfo = "", string codref = "0")
        {
            var msnError = JsonConvert.SerializeObject(ex).ToString();
            if (AditionalInfo != "")
            {
                msnError = msnError + "|" + AditionalInfo;
            }

            List<Parameters> ParametrosLog = new List<Parameters>
            {
                new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = 0 },
                new Parameters { ParameterName = "@RefLogging", Type = DbType.String, ParameterValue = msnError.ToString() },
                new Parameters { ParameterName = "@RefError", Type = DbType.String, ParameterValue = refError.ToString() },
                new Parameters { ParameterName = "@RefCode", Type = DbType.String, ParameterValue = codref.ToString() }
            };

            _proceduresDataEstudiantes.ExecuteSpSingleThread("dbo.PRC_SYS_LogError", ParametrosLog);

            return;
        }

        public string generateNewString(string BodyHtml, string ReplaceValues)
        {
            var NameTemplate = BodyHtml;
            var listReference = JsonConvert.DeserializeObject<Dictionary<string, string>>(ReplaceValues.ToString());

            foreach (var datList in listReference)
            {
                NameTemplate = NameTemplate.Replace('{' + datList.Key + '}', datList.Value);
            }

            return NameTemplate;
        }

        public string CallSendMail(MailOutput mailSend, ConfigMail MailModule)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.Host = MailModule.ServidorSmtp.ToString();
                smtp.Port = Convert.ToInt32(MailModule.PuertoSMTP.ToString());
                smtp.EnableSsl = true;

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(MailModule.UsuarioSmtp.ToString(), MailModule.ClaveSmtp.ToString());

                try
                {
                    MailMessage message = new MailMessage();

                    message.From = new MailAddress(mailSend.MailFrom);
                    message.To.Add(mailSend.MailTo);
                    if (mailSend.MailCc != null)
                    {
                        message.CC.Add(mailSend.MailCc);
                    }
                    message.Subject = mailSend.MailSubject;
                    message.Body = mailSend.MailBody;
                    message.IsBodyHtml = true;

                    List<AttachRoot> fileModule = JsonConvert.DeserializeObject<List<AttachRoot>>(mailSend.MailAttach.ToString());
                    if (fileModule != null)
                    {
                        foreach (var datList in fileModule)
                        {
                            message.Attachments.Add(new Attachment(datList.DetRoot + "\\" + datList.NamFile, MediaTypeNames.Application.Octet));
                        }
                    }

                    smtp.Send(message);

                    return "Send Mail OK";

                }
                catch (Exception ex)
                {
                    SaveErrorLog("Error al enviar correo",ex);
                    return ex.Message.ToString();
                }
            }
        }

        public void generatePlaneFile(string path, string content)
        {
            File.Delete(path);

            using (FileStream fs = File.Create(path))
            {

                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
            return;
        }

        public string SizeSuffix(long value)
        {
            string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            if (value < 0) { return "-" + SizeSuffix(-value); }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue / 1024) >= 1)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n1}&nbsp;{1}", dValue, SizeSuffixes[i]);
        }

        public string dataTableToCSV(DataTable tbl, string columdelimiter, string rowdelimiter)
        {
            StringBuilder strb = new StringBuilder();

            //column headers
            strb.AppendLine(string.Join(columdelimiter, tbl.Columns.Cast<DataColumn>()
                .Select(s => "\"" + s.ColumnName + "\"")));

            //rows
            tbl.AsEnumerable().Select(s => strb.AppendLine(
                string.Join(rowdelimiter, s.ItemArray.Select(
                    i => "\"" + i.ToString() + "\"")))).ToList();

            return strb.ToString();
        }
    }
}
