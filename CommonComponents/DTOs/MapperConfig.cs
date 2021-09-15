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

            });
        }
    }
}
