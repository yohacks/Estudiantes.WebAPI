namespace Utilitary.Core.Estudiantes.Queries
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Mmbari.Encripcion;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Exceptions;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Core.Common.Utilities;
    using Utilitary.Domine;
    using Utilitary.Domine.Common;

    public class GetPasswordForgetQry : IRequest<string>
    {
        public string Username { get; set; }

        public class GetPasswordUserHandler : IRequestHandler<GetPasswordForgetQry, string>
        {
            private readonly IConfiguration _configuration;
            private readonly IDataEstudiantesDbContext _DataEstudiantesDbContext;
            private readonly IProceduresDataEstudiantes _proceduresDataEstudiantes;
            private readonly InternalServices _internalServices;

            public GetPasswordUserHandler(IConfiguration configuration, IProceduresDataEstudiantes proceduresDataEstudiantes, IDataEstudiantesDbContext DataEstudiantesDbContext, InternalServices internalServices)
            {
                _configuration = configuration;
                _DataEstudiantesDbContext = DataEstudiantesDbContext;
                _internalServices = internalServices;
                _proceduresDataEstudiantes = proceduresDataEstudiantes;
            }

            public async Task<string> Handle(GetPasswordForgetQry rq, CancellationToken cancellationToken)
            {
                try
                {
                    string desencryptedKey = "No se encontraron registros en la Base de Datos.";

                    // Consulta la información del usuario por el Username
                    List<Parameters> ParametrosSp = new List<Parameters>
                    {
                    new Parameters { ParameterName = "@Username", Type = DbType.String, ParameterValue = rq.Username }
                    };

                    UsuariosEntity UserInfo = await _proceduresDataEstudiantes.ExecuteFirstOrDefaultSpAsync<UsuariosEntity>("dbo.PRP_SEG_Usuarios", ParametrosSp, cancellationToken);
                    if (UserInfo != null)
                    {
                        if (!string.IsNullOrEmpty(UserInfo.Email))
                        {
                            // Envia el correo al usuario con las credenciales
                            List<ConfigMail> mailConfig = (List<ConfigMail>)await _proceduresDataEstudiantes.ExecuteSpAsync<ConfigMail>("dbo.PRC_SEG_Parametros", cancellationToken);
                            TemplateMail templateMail = await _DataEstudiantesDbContext.TemplateMailDbEntity.SingleOrDefaultAsync(t => t.CodTemplate == "MAILPASSWORDFORGET", cancellationToken);

                            EncryptionFactory encrypt = new EncryptionFactory();
                            desencryptedKey = encrypt.Dencrypt(UserInfo.Password, _configuration["EService:SymmetricKey"], EncryptionEnumerator.Symetric, EncryptionSymetricEnumerator.RijndaelManaged);

                            var newMailHtml = new
                            {
                                NOMBRE = UserInfo.Nombre,
                                USERNAME = UserInfo.Username,
                                PASSWORD = desencryptedKey,
                            };
                            string htmlJson = JsonConvert.SerializeObject(newMailHtml).ToString();

                            MailOutput newMail = new MailOutput
                            {
                                MailId = templateMail.Id,
                                MailFrom = mailConfig[0].CorreoSmtp,
                                MailTo = UserInfo.Email,
                                MailCc = null,
                                MailSubject = _internalServices.generateNewString(templateMail.SubjectTemplate, htmlJson),
                                MailBody = _internalServices.generateNewString(templateMail.BodyTemplate, htmlJson),
                                MailResponse = templateMail.Id.ToString(),
                                MailStatus = 1,
                                MailSend = DateTime.Now,
                                MailAttach = "",
                            };

                            string responseMail = _internalServices.CallSendMail(newMail, mailConfig[0]);
                        }
                        else
                        {
                            desencryptedKey = "El usuario no tiene registrado ningún correo en el sistema.";
                        }

                    }
                    return desencryptedKey;

                }
                catch (Exception ex)
                {
                    _internalServices.SaveErrorLog("ERROR GetPasswordForgetQry", ex);
                    throw new NotFoundException(nameof(GetPasswordForgetQry), ex);
                }
            }
        }
    }
}