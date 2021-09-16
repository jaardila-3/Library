using AutoMapper;
using DataAccess.Models;

namespace CommonComponents.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration Configuration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Areas, AreasDTO>();
                cfg.CreateMap<AreasDTO, Areas>();

                cfg.CreateMap<Libros, LibrosDTO>();
                cfg.CreateMap<LibrosDTO, Libros>();

                cfg.CreateMap<Usuarios, UsuariosDTO>();
                cfg.CreateMap<UsuariosDTO, Usuarios>();

                cfg.CreateMap<Prestamos, PrestamosDTO>();
                cfg.CreateMap<PrestamosDTO, Prestamos>();

                cfg.CreateMap<DetallePrestamos, DetallePrestamosDTO>();
                cfg.CreateMap<DetallePrestamosDTO, DetallePrestamos>();

                cfg.CreateMap<Sanciones, SancionesDTO>();
                cfg.CreateMap<SancionesDTO, Sanciones>();

            });
        }
    }
}
