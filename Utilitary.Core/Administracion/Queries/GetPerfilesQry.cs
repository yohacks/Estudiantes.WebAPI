namespace Utilitary.Core.Administracion.Queries
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Core.Common.Interfaces.Persistence;
    using Utilitary.Domine;

    public class GetPerfilesQry : IRequest<IList<PerfilEntity>>
    {
        public class GetPerfilesHandler : IRequestHandler<GetPerfilesQry, IList<PerfilEntity>>
        {
            private readonly IDataEstudiantesDbContext _dataEstudiantesDbContext;
            public readonly IMapper _mapper;

            public GetPerfilesHandler(IDataEstudiantesDbContext dataEstudiantesDbContext, IMapper mapper)
            {
                _dataEstudiantesDbContext = dataEstudiantesDbContext;
                _mapper = mapper;
            }

            public async Task<IList<PerfilEntity>> Handle(GetPerfilesQry request, CancellationToken cancellationToken)
            {
                var lst = await _dataEstudiantesDbContext.PerfilDbEntity
                     .Where(a => a.Activo == 1)
                     .ProjectTo<PerfilEntity>(_mapper.ConfigurationProvider)
                     .OrderBy(a => a.IdPerfil)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);

                return lst;
            }
        }
    }
}
