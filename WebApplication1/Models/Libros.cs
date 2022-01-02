using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Libros
    {
        [Key]

      
        public int LibrosID { get; set; }

        [Display(Name = "ISNB del Libro")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(ErrorMessage = "El ISNB del Libro es obligatorio.")]
        
        [StringLength(50, ErrorMessage = "El ISNB del libro no puede superar los 50 caracteres")]
        public string LibrosISNB{ get; set; }

        [Display(Name = "Título del Libro")]
        [Required(ErrorMessage = "El Titulo del Libro es obligatorio.")]
        
        [StringLength(100, ErrorMessage = "El Titulo del Libro no puede superar los 100 caracteres")]
        public string LibroTitulo { get; set; }

        [Display(Name = "Reseña")]
        
        
        
        public string LibroResenia { get; set; }

        [Display(Name = "Fecha de Publicación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime LibroFechaPublicacion { get; set; }

        [Display(Name = "Estado del Libro")]
        public  EstadoLibros EstadoLibros { get; set; }

        [Display(Name = "Autor")]
        
        public int AutoresID { get; set; }
        [Display(Name = "Autor")]
        public virtual Autores Autores { get; set; }

        [Display(Name = "Editorial")]
        
        public int EditorialesID { get; set; }
        public virtual Editoriales Editoriales { get; set; }

        [Display(Name = "Género")]
        public int GenerosID { get; set; }
        public virtual Generos Generos { get; set; }

        [Display(Name = "Sector")]
        public int seccionesID { get; set; }
        public virtual Secciones Secciones { get; set; }

        public virtual ICollection<PrestamosDetalles> PrestamosDetalles { get; set; }
    }

     public enum EstadoLibros
    {
        Disponible,
        Prestado
    }

}