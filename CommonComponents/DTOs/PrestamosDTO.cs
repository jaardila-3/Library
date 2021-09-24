using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommonComponents.DTOs
{
    public class PrestamosDTO
    {        
        public System.Guid pre_codigo { get; set; }
        [Required]
        [Display(Name = "Fecha préstamo")]
        public System.DateTime pre_fecha { get; set; }
        [Required]
        [Display(Name = "Usuario")]
        public string pre_usuario { get; set; }

        public bool pre_vigente { get; set; }

        public virtual ICollection<DetallePrestamosDTO> DetallePrestamos { get; set; }
        public virtual UsuariosDTO Usuarios { get; set; }


        //creamos atributo para fecha en formato corto
        //[Display(Name = "Fecha préstamo")]
        //public string fecha_corta { get => $"{pre_fecha.ToShortDateString() }"; }
    }
}
