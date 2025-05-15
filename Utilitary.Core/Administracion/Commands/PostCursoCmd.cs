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
    public class PostCursoCmd : IRequest<bool>
    {
        public int IdMateria { get; set; }
        public int IdDocente { get; set; }

        public class PostCursoHandler : IRequestHandler<PostCursoCmd, bool>
        {
            private readonly IProceduresDataEstudiantes _proceduresUtilitary;

            public PostCursoHandler(IProceduresDataEstudiantes proceduresUtilitary)
            {
                _proceduresUtilitary = proceduresUtilitary;
            }

            public async Task<bool> Handle(PostCursoCmd request, CancellationToken cancellationToken)
            {
                List<Parameters> ParametrosSp = new List<Parameters>
                {
                    new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = 3 },
                    new Parameters { ParameterName = "@IdMateria", Type = DbType.Int32, ParameterValue = request.IdMateria },
                    new Parameters { ParameterName = "@IdDocente", Type = DbType.Int32, ParameterValue = request.IdDocente },
                };

                var rslt = await _proceduresUtilitary.ExecuteFirstOrDefaultSpAsync<bool>("dbo.PRP_CCB_Cursos", ParametrosSp, cancellationToken);

                return rslt;
            }
        }
    }
}
