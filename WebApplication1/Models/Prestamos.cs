using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Prestamos
    {
       [Key]

        [Display(Name = "Prestamo")]
        public int  PrestamosID { get; set;}

        [Display(Name = "Fecha del Prestamo")]
        [DataType(DataType.Date)]
        public DateTime PrestamosFecha { get; set; }

        [Display(Name = "Fecha de Devolucion")]
        [DataType(DataType.Date)]
        public DateTime FechaDevolucion { get; set; }

        [Display(Name = "Socio")]
        public int SociosID { get; set; }
        public virtual Socios Socios { get; set; }

        public virtual ICollection<PrestamosDetalles> PrestamosDetalles { get; set; }

    }
}