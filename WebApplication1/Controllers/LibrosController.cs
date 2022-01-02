using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LibrosController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Libros
        public ActionResult Index()
        {
            var libros = db.Libros.Include(l => l.Autores).Include(l => l.Editoriales).Include(l => l.Generos).Include(l => l.Secciones);
            return View(libros.ToList());
        }

        // GET: Libros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libros libros = db.Libros.Find(id);
            if (libros == null)
            {
                return HttpNotFound();
            }
            return View(libros);
        }

        // GET: Libros/Create
        public ActionResult Create()
        {
            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombre");
            ViewBag.EditorialesID = new SelectList(db.Editoriales, "EditorialesID", "EditorialesNombre");
            ViewBag.GenerosID = new SelectList(db.Generos, "GenerosID", "GenerosNombre");

            var secciones = (from a in db.Secciones where a.Eliminado == false select a).ToList();
            ViewBag.seccionesID = new SelectList(db.Secciones, "seccionesID", "seccionesNombre");
            return View();
        }

        // POST: Libros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LibrosID,LibrosISNB,LibroTitulo,LibroResenia,LibroFechaPublicacion,EstadoLibros,AutoresID,EditorialesID,GenerosID,seccionesID")] Libros libros)
        {
            if (ModelState.IsValid)
            {
                db.Libros.Add(libros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombre", libros.AutoresID);
            ViewBag.EditorialesID = new SelectList(db.Editoriales, "EditorialesID", "EditorialesNombre", libros.EditorialesID);
            ViewBag.GenerosID = new SelectList(db.Generos, "GenerosID", "GenerosNombre", libros.GenerosID);

            var secciones = (from a in db.Secciones where a.Eliminado == false select a).ToList();
            ViewBag.seccionesID = new SelectList(secciones, "seccionesID", "seccionesNombre", libros.seccionesID);
            return View(libros);
        }

        // GET: Libros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libros libros = db.Libros.Find(id);
            if (libros == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombre", libros.AutoresID);
            ViewBag.EditorialesID = new SelectList(db.Editoriales, "EditorialesID", "EditorialesNombre", libros.EditorialesID);
            ViewBag.GenerosID = new SelectList(db.Generos, "GenerosID", "GenerosNombre", libros.GenerosID);

            var secciones = (from a in db.Secciones where a.Eliminado == false select a).ToList();
            ViewBag.seccionesID = new SelectList(secciones, "seccionesID", "seccionesNombre", libros.seccionesID);
            return View(libros);
        }

        // POST: Libros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LibrosID,LibrosISNB,LibroTitulo,LibroResenia,LibroFechaPublicacion,EstadoLibros,AutoresID,EditorialesID,GenerosID,seccionesID")] Libros libros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutoresID = new SelectList(db.Autores, "AutoresID", "AutoresNombre", libros.AutoresID);
            ViewBag.EditorialesID = new SelectList(db.Editoriales, "EditorialesID", "EditorialesNombre", libros.EditorialesID);
            ViewBag.GenerosID = new SelectList(db.Generos, "GenerosID", "GenerosNombre", libros.GenerosID);

            var secciones = (from a in db.Secciones where a.Eliminado == false select a).ToList();
            ViewBag.seccionesID = new SelectList(secciones, "seccionesID", "seccionesNombre", libros.seccionesID);
            return View(libros);
        }

        // GET: Libros/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Libros libros = db.Libros.Find(id);
        //    if (libros == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(libros);
        //}

        // POST: Libros/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libros libros = db.Libros.Find(id);
            db.Libros.Remove(libros);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
