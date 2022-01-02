using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WebApplication1Context() : base("name=WebApplication1Context")
        {
        }

        public System.Data.Entity.DbSet<WebApplication1.Models.Autores> Autores { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Secciones> Secciones { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Editoriales> Editoriales { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Generos> Generos { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Socios> Socios { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Libros> Libros { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Prestamos> Prestamos { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.PrestamosDetalles> PrestamosDetalles { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.PrestamosDetallesTemp> PrestamosDetallesTemp { get; set; }
    }
}
