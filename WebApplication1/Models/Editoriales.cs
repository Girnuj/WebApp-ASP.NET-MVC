using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Editoriales
    {

        [Key]
        public int EditorialesID { get; set; }

        [Display(Name = "Nombre de la Editorial")]
        [Required(ErrorMessage = "El Nombre de la Editorial es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Nombre de la Editorial no puede superar los 50 caracteres")]
        public string EditorialesNombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}