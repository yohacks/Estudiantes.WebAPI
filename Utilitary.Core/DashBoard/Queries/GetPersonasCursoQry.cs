namespace Utilitary.Core.DashBoard.Queries
{
    using MediatR;
    using Microsoft.Extensions.Configuration;
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

    public class GetPersonasCursoQry : IRequest<List<PersonasCursoModel>>
    {
        public int Referencia { get; set; }
        public int? IdUsuario { get; set; }

        public class CitasDiaHandler : IRequestHandler<GetPersonasCursoQry, List<PersonasCursoModel>>
        {
            private readonly IProceduresDataEstudiantes _proceduresDataEstudiantes;
            private readonly IConfiguration _configuration;
            private readonly InternalServices _internalService;

            public CitasDiaHandler(IProceduresDataEstudiantes proceduresDataEstudiantes, IConfiguration configuration, InternalServices internalService)
            {
                _proceduresDataEstudiantes = proceduresDataEstudiantes;
                _configuration = configuration;
                _internalService = internalService;
            }

            public async Task<List<PersonasCursoModel>> Handle(GetPersonasCursoQry rq, CancellationToken cancellationToken)
            {
                try
                {
                    //Se declaran las variables que se enviaran al SP
                    List<Parameters> ParametrosSp = new List<Parameters> {
                    new Parameters { ParameterName = "@Referencia", Type = DbType.String, ParameterValue = rq.Referencia },
                    new Parameters { ParameterName = "@IdUsuario", Type = DbType.String, ParameterValue = rq.IdUsuario },
                    };

                    //Se ejecuta procedimiento almacenado que trae la cantidad de ordenes y valores de venta
                    List<PersonasCursoModel> rslt = (List<PersonasCursoModel>)await _proceduresDataEstudiantes.ExecuteSpAsync<PersonasCursoModel>("dbo.PRP_CCB_Dashboard", ParametrosSp, cancellationToken);

                    return rslt;

                }
                catch (Exception ex)
                {
                    _internalService.SaveErrorLog("ERROR GetCitasDiaQry", ex);
                    throw new NotFoundException(nameof(GetPersonasCursoQry), rq.IdUsuario);
                }
            }
        }
    }
}