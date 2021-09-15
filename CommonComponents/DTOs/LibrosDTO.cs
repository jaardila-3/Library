using System.ComponentModel.DataAnnotations;

namespace CommonComponents.DTOs
{
    public class LibrosDTO
    {
        [Required]
        [Display(Name = "Código libro")]
        public int lib_codigo { get; set; }
        [Required]
        [Display(Name = "Nombre libro")]
        public string lib_nombre { get; set; }
        [Display(Name = "Número de páginas")]
        public int? lib_num_pag { get; set; }
        [Required]
        [Display(Name = "Autor libro")]
        public string lib_autor { get; set; }
        [Required]
        [Display(Name = "Editorial libro")]
        public string lib_editorial { get; set; }
        [Required]
        [Display(Name = "Área que pertenece")]
        public int lib_area { get; set; }
        [Display(Name = "Nombre área")]
        public virtual AreasDTO Areas { get; set; }
    }
}
