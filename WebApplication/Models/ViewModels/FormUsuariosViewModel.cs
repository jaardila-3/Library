﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.ViewModels
{
    public class FormUsuariosViewModel
    {
        [Required]
        [Display(Name ="No. Documento")]
        public string Documento { get; set; }
        [Required]
        [Display(Name = "Nombre y Apellido")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Dirección Residencia")]
        public string Direccion { get; set; }
        [Required]
        [Display(Name = "No. Celular o Teléfono")]
        public string Telefono { get; set; }
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]

        public string Correo { get; set; }
        //public string Estado { get; set; } //Activo - Sancionado
    }
}