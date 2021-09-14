using System;
using System.ComponentModel.DataAnnotations;

namespace CommonComponents.DTOs
{
    public class AreasDTO
    {
        [Required]
        [Display(Name = "Código área")]
        public int are_codigo { get; set; }
        [Required]
        [Display(Name = "Nombre área")]
        public string are_nombre { get; set; }
        [Required]
        [Display(Name = "Tiempo máximo")]
        public int are_tiempo { get; set; }
    }
}
