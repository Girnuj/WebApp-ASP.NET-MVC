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
    public class SeccionesController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Secciones
        public ActionResult Index()
        {
            var secciones = (from a in db.Secciones where a.Eliminado == false select a).ToList();
            return View(secciones);
        }

        // GET: Secciones/Details/5
        //  public ActionResult Details(int? id)
        //  {
        //    if (id == null)
        //    {
        //       return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //  }
        //  Secciones secciones = db.Secciones.Find(id);
        //  if (secciones == null)
        //  {
        //      return HttpNotFound();
        //   }
        //   return View(secciones);
        //  }

        // GET: Secciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Secciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "seccionesID,seccionesNombre")] Secciones secciones)
        {
            var OtraSeccion = false;

            secciones.seccionesNombre = secciones.seccionesNombre.ToLower();

            var mismoNombreSeccion = (from a in db.Secciones select a).ToList();
            foreach ( var item in mismoNombreSeccion)
            {
                if (item.seccionesNombre == secciones.seccionesNombre)
                {
                    OtraSeccion = true;


                }

            }

            if (OtraSeccion == false)
            {
                if (ModelState.IsValid)
                {
                    db.Secciones.Add(secciones);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

           // if (ModelState.IsValid)
           // {
              //  db.Secciones.Add(secciones);
             //   db.SaveChanges();
              //  return RedirectToAction("Index");
           // }

            return View(secciones);
        }

        // GET: Secciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = db.Secciones.Find(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }

        // POST: Secciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "seccionesID,seccionesNombre")] Secciones secciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secciones);
        }

        // GET: Secciones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Secciones secciones = db.Secciones.Find(id);
        //    if (secciones == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(secciones);
        //}

        // POST: Secciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Secciones secciones = db.Secciones.Find(id);
            secciones.Eliminado = true;
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
