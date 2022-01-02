using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PrestamosDetalles
    {
        [Key]
        public int PrestamosDetallesID { get; set; }
        
        [Display(Name = "Prestamo")]
        public int PrestamosID { get; set; }
        public virtual Prestamos Prestamos { get; set; }
        
        [Display(Name = "Libros")]
        public int LibrosID { get; set; }
        public virtual Libros Libros { get; set; }
    }
}