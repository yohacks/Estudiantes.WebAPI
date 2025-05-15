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
    public class GetMateriasQry : IRequest<IEnumerable<object>>
    {

        public class GetMateriasHandler : IRequestHandler<GetMateriasQry, IEnumerable<object>>
        {
            private readonly IProceduresDataEstudiantes _proceduresUtilitary;

            public GetMateriasHandler(IProceduresDataEstudiantes proceduresUtilitary)
            {
                _proceduresUtilitary = proceduresUtilitary;
            }

            public async Task<IEnumerable<object>> Handle(GetMateriasQry request, CancellationToken cancellationToken)
            {
                List<Parameters> ParametrosSp = new List<Parameters>
                {
                    new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = 2 },
                };

                IEnumerable<object> rslt = new List<object>();

                rslt = await _proceduresUtilitary.ExecuteSpAsync<object>("dbo.PRP_CCB_Cursos", ParametrosSp, cancellationToken);

                return rslt;
            }
        }
    }
}
