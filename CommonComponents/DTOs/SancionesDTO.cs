using System.ComponentModel.DataAnnotations;

namespace CommonComponents.DTOs
{
    public class SancionesDTO
    {
        public System.Guid san_codigo { get; set; }
        public System.Guid san_prestamo { get; set; }
        [Required]
        [Display(Name = "Días de sanción")]
        public int san_dias_sancion { get; set; }
        [Required]
        [Display(Name = "Fecha de inicio")]
        public System.DateTime san_fecha_inicio { get; set; }
        [Required]
        [Display(Name = "Fecha final")]
        public System.DateTime san_fecha_fin { get; set; }

        public virtual PrestamosDTO Prestamos { get; set; }
    }
}
