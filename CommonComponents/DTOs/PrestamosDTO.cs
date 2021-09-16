using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommonComponents.DTOs
{
    public class PrestamosDTO
    {        
        public System.Guid pre_codigo { get; set; }
        [Required]
        [Display(Name = "Fecha préstamo")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime pre_fecha { get; set; }
        [Required]
        [Display(Name = "Usuario que presta")]
        public string pre_usuario { get; set; }

        public virtual ICollection<DetallePrestamosDTO> DetallePrestamos { get; set; }
        public virtual UsuariosDTO Usuarios { get; set; }
    }
}
