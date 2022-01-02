using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Generos
    {
        [Key]
        public int GenerosID { get; set; }

        [Display(Name = "Nombre del Género")]
        [Required(ErrorMessage = "El nombre del Género es obligatorio.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No se permiten números")]
        [StringLength(50, ErrorMessage = "El Nombre del Género no puede superar los 50 caracteres")]
        public string GenerosNombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }

}