namespace Utilitary.Core.Estudiantes.Queries
{
    using MediatR;
    using Newtonsoft.Json;
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
    public class GetMenuQry : IRequest<IEnumerable<MenuModel>>
    {
        public int IdPerfil { get; set; }
        public int Referencia { get; set; }

        public class MenuListHandler : IRequestHandler<GetMenuQry, IEnumerable<MenuModel>>
        {
            private readonly IProceduresDataEstudiantes _proceduresDataEstudiantes;
            private readonly InternalServices _internalService;

            public MenuListHandler(IProceduresDataEstudiantes proceduresDataEstudiantes, InternalServices internalService)
            {
                _proceduresDataEstudiantes = proceduresDataEstudiantes;
                _internalService = internalService;
            }

            public async Task<IEnumerable<MenuModel>> Handle(GetMenuQry request, CancellationToken cancellationToken)
            {
                try
                {
                    //Se declaran las variables que se enviaran al SP
                    List<Parameters> ParametrosSp = new List<Parameters>
                    {
                    new Parameters { ParameterName = "@IdPerfil", Type = DbType.Int32, ParameterValue = request.IdPerfil }
                    };

                    //Se ejecuta procedimiento almacenado que trae los menus y submenus por perfil
                    var rslt = await _proceduresDataEstudiantes.ExecuteSpAsync<MenuModel>("dbo.PRP_ConsultarMenu", ParametrosSp, cancellationToken);
                    List<MenuModel> menu = new List<MenuModel>();

                    //Si el menu tiene submenus los encapsula
                    if (request.Referencia == 1)
                    {
                        menu = rslt.Where(x => x.ParentId == null).ToList();
                        foreach (var item in menu)
                        {
                            var submenu = rslt.Where(x => x.ParentId == item.IdOpcion).ToList();
                            if (submenu.Count == 0)
                            {
                                item.Items = null;
                            }
                            else
                            {
                                item.Items = submenu;

                            }
                        }
                    }
                    //Si el menu no tiene submenus devuelve el listado completo sin modificarlo
                    else
                    {
                        menu = rslt.ToList();
                    }

                    return menu;

                }
                catch (Exception ex)
                {
                    _internalService.SaveErrorLog("ERROR UpdateCierreDiarioCmd", ex);
                    throw new NotFoundException(nameof(GetMenuQry), request.IdPerfil);
                }
            }
        }
    }
}