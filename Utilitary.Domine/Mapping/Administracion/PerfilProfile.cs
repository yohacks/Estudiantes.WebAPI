namespace Utilitary.Domine.Mapping
{
    using AutoMapper;
    using Utilitary.Domine;
    public class PerfilProfile : Profile
    {
        public PerfilProfile()
        {
            CreateMap<PerfilEntity, PerfilEntity>();
        }
    }
}
