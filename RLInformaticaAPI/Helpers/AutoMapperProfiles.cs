using AutoMapper;
using RLInformaticaAPI.DTOS;
using RLInformaticaAPI.Entidades;

namespace RLInformaticaAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Dispositivo, DispositivoDTO>().ReverseMap();
            CreateMap<DispositivoCreacionDTO, Dispositivo>();

            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<MarcaCreacionDTO, Marca>();

            CreateMap<Reparacion, ReparacionDTO>()
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.Nombre))
                .ForMember(dest => dest.Dispositivo, opt => opt.MapFrom(src => src.Dispositivo.Nombre))
                .ForMember(dest => dest.Empleado, opt => opt.MapFrom(src => src.Empleado.UserName));

            CreateMap<ReparacionDTO, Reparacion>();
            CreateMap<ReparacionCreacionDTO, Reparacion>();
            CreateMap<ReparacionPatchDTO, Reparacion>().ReverseMap();   
        }
    }
}
