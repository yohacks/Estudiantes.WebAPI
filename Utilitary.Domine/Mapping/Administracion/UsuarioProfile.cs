namespace Utilitary.Domine.Mapping
{
    using AutoMapper;
    using Utilitary.Domine;
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuariosEntity, UsuariosEntity>();
        }
    }
}
