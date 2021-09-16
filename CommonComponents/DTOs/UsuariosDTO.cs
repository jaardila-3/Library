using System.ComponentModel.DataAnnotations;

namespace CommonComponents.DTOs
{
    public class UsuariosDTO
    {
        [Required]
        [Display(Name = "No. Documento")]
        public string usu_documento { get; set; }
        [Required]
        [Display(Name = "Nombre y Apellido")]
        public string usu_nombre { get; set; }
        [Required]
        [Display(Name = "Dirección Residencia")]
        public string usu_direccion { get; set; }
        [Required]
        [Display(Name = "No. Celular o Teléfono")]
        public string usu_telefono { get; set; }
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string usu_correo { get; set; }
        [Display(Name = "Estado del usuario")]
        public string usu_estado { get; set; } //Activo - Sancionado
    }
}
