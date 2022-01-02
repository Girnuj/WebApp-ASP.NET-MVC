using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Secciones
    {
        [Key]
        public int seccionesID { get; set; }

        [Display(Name = "Nombre de la Sección")]
        [Required(ErrorMessage = "El Nombre de la Sección es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Nombre de la Sección no puede superar los 50 caracteres")]
        public string seccionesNombre { get; set; }

        public bool Eliminado { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}