using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Autores

    {
        [Key]
       
        [Display(Name = "Autor")]
        public int AutoresID { get; set; }

        [Display(Name = "Nombre del Autor")]
        [Required(ErrorMessage = "El Nombre del Autor es obligatorio.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No se permiten números")]
        [StringLength(50, ErrorMessage = "El Nombre no puede superar los 50 caracteres")]
        public string AutoresNombre { get; set;}

        [Display(Name = "Apellido del Autor")]
        [Required(ErrorMessage = "El Apellido del Autor es obligatorio.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No se permiten números")]
        [StringLength(50, ErrorMessage = "El Apellido del Autor no puede superar los 50 caracteres")]

        public string AutoresApellido { get; set; }


       [NotMapped]
        public string AutoresNombreCompleto {
            get
            {
                return string.Format("{0} {1}", AutoresApellido, AutoresNombre);
            }
        
        }

        public virtual ICollection<Libros> Libros { get; set; }

    }
}