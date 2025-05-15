namespace Utilitary.Domine.Mapping
{
    using AutoMapper;
    using Utilitary.Domine;
    public class UsuarioPerfilProfile : Profile
    {
        public UsuarioPerfilProfile()
        {
            CreateMap<UsuarioPerfilEntity, UsuarioPerfilEntity>();
        }

    }
}
