namespace Utilitary.Core.Administracion.Queries
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
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

    public class UpdateCursoCmd : IRequest<bool>
    {
        public int? IdInscripcion { get; set; }
        public int? IdCurso { get; set; }
        public int? IdUsuario { get; set; }
        public int? Referencia { get; set; }

        public class UpdateCursoHandler : IRequestHandler<UpdateCursoCmd, bool>
        {
            private readonly IProceduresDataEstudiantes _proceduresUtilitary;
            private readonly InternalServices _internalService;

            public UpdateCursoHandler(IProceduresDataEstudiantes proceduresUtilitary, InternalServices internalService)
            {
                _proceduresUtilitary = proceduresUtilitary;
                _internalService = internalService;
            }
            public async Task<bool> Handle(UpdateCursoCmd rq, CancellationToken cancellationToken)
            {
                try
                {

                    //Se declaran las variables que se enviaran al SP
                    List<Parameters> ParametrosSp = new List<Parameters>
                    {
                    new Parameters { ParameterName = "@IdInscripcion", Type = DbType.Int32, ParameterValue = rq.IdInscripcion },
                    new Parameters { ParameterName = "@IdCurso", Type = DbType.Int32, ParameterValue = rq.IdCurso },
                    new Parameters { ParameterName = "@IdUsuario", Type = DbType.String, ParameterValue = rq.IdUsuario },
                    new Parameters { ParameterName = "@Referencia", Type = DbType.Int32, ParameterValue = rq.Referencia },
                    };

                    var rslt = false;
                    //Se ejecuta procedimiento almacenado que guarda la novedad del cierre diario
                    rslt = await _proceduresUtilitary.ExecuteFirstOrDefaultSpAsync<bool>("dbo.PRP_COM_Cursos", ParametrosSp, cancellationToken);

                    return rslt;

                }
                catch (Exception ex)
                {
                    _internalService.SaveErrorLog("ERROR UpdateCursoCmd", ex, rq.IdUsuario.ToString());
                    throw new NotFoundException(nameof(UpdateCursoCmd), ex.Message);
                }
            }
        }
    }
}