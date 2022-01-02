using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class PrestamosController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamos = db.Prestamos.Include(p => p.Socios);
            return View(prestamos.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto");
            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Disponible select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibroTitulo");
            return View();
        }

        // POST: Prestamos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrestamosID,PrestamosFecha,FechaDevolucion,SociosID")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = db.Database.BeginTransaction()) {
                    try
                    {
                        db.Prestamos.Add(prestamos);
                        db.SaveChanges();

                        var prestamosDetallesTemp = (from a in db.PrestamosDetallesTemp select a).ToList();
                        foreach (var item in prestamosDetallesTemp) {

                            var LibroGuardar = new PrestamosDetalles
                            {
                                LibrosID = item.LibrosID,
                                PrestamosID = prestamos.PrestamosID
                            };

                            db.PrestamosDetalles.Add(LibroGuardar);
                            db.SaveChanges();

                        }

                        db.PrestamosDetallesTemp.RemoveRange(prestamosDetallesTemp);
                        db.SaveChanges();

                        transaccion.Commit();

                        return RedirectToAction("Index");
                    }
                    catch(Exception ex) {
                        transaccion.Rollback();
                    }
                }

                return RedirectToAction("Index");
            }
            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Disponible select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibroTitulo");

            ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", prestamos.SociosID);
            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Prestamos prestamos = db.Prestamos.Find(id);
        //    if (prestamos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.LibrosID = new SelectList(db.Libros, "LibrosID", "LibroTitulo");
        //    ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", prestamos.SociosID);
        //    return View(prestamos);
        //}

        //// POST: Prestamos/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        //// más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "PrestamosID,PrestamosFecha,FechaDevolucion,SociosID")] Prestamos prestamos)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(prestamos).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.LibrosID = new SelectList(db.Libros, "LibrosID", "LibroTitulo");
        //    ViewBag.SociosID = new SelectList(db.Socios, "SociosID", "SociosNombreCompleto", prestamos.SociosID);
        //    return View(prestamos);
        //}

        // GET: Prestamos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Prestamos prestamos = db.Prestamos.Find(id);
        //    if (prestamos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(prestamos);
        //}

        // POST: Prestamos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamos prestamos = db.Prestamos.Find(id);
            db.Prestamos.Remove(prestamos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult GuardarLibro(int LibrosID)
        {
            var resultado = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var libros = (from a in db.Libros where a.LibrosID == LibrosID select a).SingleOrDefault();
                    libros.EstadoLibros = EstadoLibros.Prestado;
                    db.SaveChanges();

                    var LibroGuardar = new PrestamosDetallesTemp
                    {
                        LibrosID = libros.LibrosID,
                        LibroTitulo = libros.LibroTitulo
                    };

                    db.PrestamosDetallesTemp.Add(LibroGuardar);
                    db.SaveChanges();

                    transaccion.Commit();

                    resultado = true;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false;
                }


            }

            var librosCombo = (from a in db.Libros where a.EstadoLibros == EstadoLibros.Disponible select a).ToList();
            ViewBag.LibrosID = new SelectList(librosCombo, "LibrosID", "LibroTitulo");

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }




        public JsonResult BuscarLibros()
        {
            List<PrestamosDetallesTemp> ListadoPrestamosDetallesTemp = new List<PrestamosDetallesTemp>();
           
            var librosTemporal = (from a in db.PrestamosDetallesTemp select a).ToList();

            foreach (var item in librosTemporal) {

                var LibroBuscar = new PrestamosDetallesTemp
                {
                    PrestamosDetallesTempID = item.PrestamosDetallesTempID, 
                    LibrosID = item.LibrosID,
                    LibroTitulo = item.LibroTitulo
                };

                ListadoPrestamosDetallesTemp.Add(LibroBuscar);
                
            }

            return Json(ListadoPrestamosDetallesTemp, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CancelarPrestamo()
        {

            var resultado = true;
            var pretamosDetallesTemp = (from a in db.PrestamosDetallesTemp select a).ToList();

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {

                    foreach (var item in pretamosDetallesTemp) {

                        var libro = (from a in db.Libros where a.LibrosID == item.LibrosID select a).SingleOrDefault();
                        libro.EstadoLibros = EstadoLibros.Disponible;
                        db.SaveChanges();
                    }

                    db.PrestamosDetallesTemp.RemoveRange(pretamosDetallesTemp);
                    db.SaveChanges();

                    transaccion.Commit();

                    resultado = true;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false;
                }


            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarLibro(int PrestamosDetallesTempID)
        {

            var resultado = true;
            var pretamosDetallesTemp = (from a in db.PrestamosDetallesTemp select a).ToList();

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    PrestamosDetallesTemp prestamosDetallesTemp = db.PrestamosDetallesTemp.Find(PrestamosDetallesTempID);
                    db.PrestamosDetallesTemp.Remove(prestamosDetallesTemp);

                    var libro = (from a in db.Libros where a.LibrosID == prestamosDetallesTemp.LibrosID select a).SingleOrDefault();
                    libro.EstadoLibros = EstadoLibros.Disponible;
                    db.SaveChanges();

                    transaccion.Commit();

                    resultado = true;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    resultado = false;
                }

            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
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
