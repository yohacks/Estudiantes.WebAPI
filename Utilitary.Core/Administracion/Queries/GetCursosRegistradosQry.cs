namespace Utilitary.Core.Administracion.Queries
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Exceptions;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Core.Common.Utilities;
    using Utilitary.Domine;
    using Utilitary.Domine.Common;

    public class GetCursosRegistradosQry : IRequest<IEnumerable<object>>
    {
        public int IdUsuario { get; set; }
        public int Referencia { get; set; }

        public class GetCursosRegistradosHandler : IRequestHandler<GetCursosRegistradosQry, IEnumerable<object>>
        {
            private readonly IProceduresDataEstudiantes _proceduresUtilitary;
            private readonly InternalServices _internalService;

            public GetCursosRegistradosHandler(IProceduresDataEstudiantes proceduresUtilitary, InternalServices internalService)
            {
                _proceduresUtilitary = proceduresUtilitary;
                _internalService = internalService;
            }

            public async Task<IEnumerable<object>> Handle(GetCursosRegistradosQry request, CancellationToken cancellationToken)
            {
                try
                {
                    //Se declaran las variables que se enviaran al SP
                    List<Parameters> ParametrosSp = new List<Parameters>
                    {
                        new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = request.Referencia },
                        new Parameters { ParameterName = "@IdEstudiante", Type = DbType.Int32, ParameterValue = request.IdUsuario },
                    };
                    IEnumerable<object> rslt = new List<object>();

                    //Se ejecuta procedimiento almacenado que trae el consolidado de depositos en un periodo de tiempo
                    rslt = await _proceduresUtilitary.ExecuteSpAsync<object>("dbo.PRC_COM_CursosRegistrados", ParametrosSp, cancellationToken);


                    return rslt;

                }
                catch (Exception ex)
                {
                    _internalService.SaveErrorLog("ERROR GetCursosRegistradosQry", ex, request.IdUsuario.ToString());
                    throw new NotFoundException(nameof(GetCursosRegistradosQry), ex.Message);
                }
            }
        }
    }

}
