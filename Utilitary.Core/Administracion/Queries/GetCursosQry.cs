namespace Utilitary.Core.Administracion.Queries
{
    using MediatR;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Domine;
    using Utilitary.Domine.Common;
    public class GetCursosQry : IRequest<IEnumerable<object>>
    {

        public class GetCursosHandler : IRequestHandler<GetCursosQry, IEnumerable<object>>
        {
            private readonly IProceduresDataEstudiantes _proceduresUtilitary;

            public GetCursosHandler(IProceduresDataEstudiantes proceduresUtilitary)
            {
                _proceduresUtilitary = proceduresUtilitary;
            }

            public async Task<IEnumerable<object>> Handle(GetCursosQry request, CancellationToken cancellationToken)
            {
                List<Parameters> ParametrosSp = new List<Parameters>
                {
                    new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = 4 },
                };

                IEnumerable<object> rslt = new List<object>();

                rslt = await _proceduresUtilitary.ExecuteSpAsync<object>("dbo.PRP_CCB_Cursos", ParametrosSp, cancellationToken);

                return rslt;
            }
        }
    }
}
