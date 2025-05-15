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
    public class GetInfoUsuarioQry : IRequest<List<ListadoInfoUsuarioModel>>
    {
        public class GetListaActividadesListHandler : IRequestHandler<GetInfoUsuarioQry, List<ListadoInfoUsuarioModel>>
        {
            private readonly IProceduresDataEstudiantes _proceduresUtilitary;

            public GetListaActividadesListHandler(IProceduresDataEstudiantes proceduresUtilitary)
            {
                _proceduresUtilitary = proceduresUtilitary;
            }

            public async Task<List<ListadoInfoUsuarioModel>> Handle(GetInfoUsuarioQry request, CancellationToken cancellationToken)
            {
                List<Parameters> ParametrosSp = new List<Parameters>
                {
                    new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = 1 },
                };

                InfoUsuarioModel rslt = await _proceduresUtilitary.ExecuteFirstOrDefaultSpAsync<InfoUsuarioModel>("dbo.PRP_CCB_Administracion", ParametrosSp, cancellationToken);

                List<ListadoInfoUsuarioModel> data = JsonConvert.DeserializeObject<List<ListadoInfoUsuarioModel>>(rslt.Usuario.ToString());

                return data;
            }
        }
    }
}
