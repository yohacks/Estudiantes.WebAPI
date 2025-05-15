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
    public class PostMateriaCmd : IRequest<bool>
    {
        public string Nombre { get; set; }
        public int Creditos { get; set; }

        public class PostMateriaHandler : IRequestHandler<PostMateriaCmd, bool>
        {
            private readonly IProceduresDataEstudiantes _proceduresUtilitary;

            public PostMateriaHandler(IProceduresDataEstudiantes proceduresUtilitary)
            {
                _proceduresUtilitary = proceduresUtilitary;
            }

            public async Task<bool> Handle(PostMateriaCmd request, CancellationToken cancellationToken)
            {
                List<Parameters> ParametrosSp = new List<Parameters>
                {
                    new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = 1 },
                    new Parameters { ParameterName = "@Nombre", Type = DbType.String, ParameterValue = request.Nombre },
                    new Parameters { ParameterName = "@Creditos", Type = DbType.Int32, ParameterValue = request.Creditos },
                };

                var rslt = await _proceduresUtilitary.ExecuteFirstOrDefaultSpAsync<bool>("dbo.PRP_CCB_Cursos", ParametrosSp, cancellationToken);

                return rslt;
            }
        }
    }
}
