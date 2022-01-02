using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PrestamosDetallesTemp
    {
       [Key]
        public int PrestamosDetallesTempID { get; set; }

        public int LibrosID { get; set; }

        [Display(Name = "Título del Libro")]
       
        public string LibroTitulo { get; set; }


    }
}