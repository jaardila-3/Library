using System;
using System.ComponentModel.DataAnnotations;

namespace CommonComponents.DTOs
{
    public class DetallePrestamosDTO
    {
        public Guid dtp_prestamo { get; set; }
        [Required]
        [Display(Name = "Libro")]
        public int dtp_libro { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        public int dtp_cantidad { get; set; }
        [Required]
        [Display(Name = "Fecha de entrega")]
        public DateTime dtp_fecha_fin { get; set; }
        [Display(Name = "Fecha devolución")]
        public DateTime? dtp_fecha_dev { get; set; }

        public virtual LibrosDTO Libros { get; set; }
        public virtual PrestamosDTO Prestamos { get; set; }
    }
}
