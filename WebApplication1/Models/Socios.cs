using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Socios
    {
        [Key]
        public int SociosID { get; set; }

        [Display(Name = "Nombre del Socio")]
        [Required(ErrorMessage = "El nombre del Socio es obligatorio.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No se permiten números")]
        [StringLength(70, ErrorMessage = "El Nombre del Socio no puede superar los 70 caracteres")]
        public string SociosNombre { get; set; }

        [Display(Name = "Apellido del Socio")]
        [Required(ErrorMessage = "El Apellido del Socio es obligatorio.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No se permiten números")]
        [StringLength(75, ErrorMessage = "El Apellido del Socio no puede superar los 75 caracteres")]
        public string SociosApellido { get; set; }

        [Display(Name = "Dirección del Socio")]

        [StringLength(100, ErrorMessage = "La Direccion no puede superar los 100 caracteres")]
        public string SociosDireccion { get; set; }

        [Display(Name = "Número de Teléfono")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(20, ErrorMessage = "El Teléfono no puede superar los 20 caracteres")]

        public string SociosTeléfeno { get; set; }

        [Display(Name = "Fecha de Nacimiento")]

        [DataType(DataType.Date)]
        public DateTime SocioFechaNacimiento { get; set; }

        [NotMapped]
        public string SociosNombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", SociosApellido, SociosNombre);
            }

        }

        [NotMapped]
        public int SociosEdad
        {
            get
            {
                return DateTime.Now.Year - SocioFechaNacimiento.Year;
            }

        }

        public virtual ICollection<Prestamos> Prestamos { get; set; }

    }

}