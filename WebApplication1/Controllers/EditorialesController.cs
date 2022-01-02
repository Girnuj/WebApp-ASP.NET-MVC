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
    public class EditorialesController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Editoriales
        public ActionResult Index()
        {
            return View(db.Editoriales.ToList());
        }

        // GET: Editoriales/Details/5
       // public ActionResult Details(int? id)
      //  {
        //    if (id == null)
        //    {
         //       return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         //   }
         //   Editoriales editoriales = db.Editoriales.Find(id);
         //   if (editoriales == null)
          //  {
           //     return HttpNotFound();
          //  }
          //  return View(editoriales);
       // }

        // GET: Editoriales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditorialesID,EditorialesNombre")] Editoriales editoriales)
        {

            var OtroEditorial = false;

            editoriales.EditorialesNombre = editoriales.EditorialesNombre.ToLower();

            var mismoNombreEditorial = (from a in db.Editoriales select a).ToList();
            foreach (var item in mismoNombreEditorial)
            {
                if (item.EditorialesNombre == editoriales.EditorialesNombre)
                {
                    OtroEditorial = true;


                }

            }

            if (OtroEditorial == false)
            {
                if (ModelState.IsValid)
                {
                    db.Editoriales.Add(editoriales);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }


            //if (ModelState.IsValid)
            //{
            //  db.Editoriales.Add(editoriales);
            // db.SaveChanges();
            // return RedirectToAction("Index");
            //}

            return View(editoriales);
        }

        // GET: Editoriales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editoriales editoriales = db.Editoriales.Find(id);
            if (editoriales == null)
            {
                return HttpNotFound();
            }
            return View(editoriales);
        }

        // POST: Editoriales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditorialesID,EditorialesNombre")] Editoriales editoriales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editoriales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editoriales);
        }

        // GET: Editoriales/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Editoriales editoriales = db.Editoriales.Find(id);
        //    if (editoriales == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(editoriales);
        //}

        // POST: Editoriales/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Editoriales editoriales = db.Editoriales.Find(id);
            db.Editoriales.Remove(editoriales);
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
