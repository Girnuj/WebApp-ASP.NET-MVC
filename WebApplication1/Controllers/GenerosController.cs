﻿using System;
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
    public class GenerosController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Generos
        public ActionResult Index(string MensajeDevuelto)
        {
            ViewBag.MensajeDevuelto = MensajeDevuelto;
            return View(db.Generos.ToList());
        }

        // GET: Generos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generos generos = db.Generos.Find(id);
            if (generos == null)
            {
                return HttpNotFound();
            }
            return View(generos);
        }

        // GET: Generos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenerosID,GenerosNombre")] Generos generos)
        {
            if (ModelState.IsValid)
            {
                db.Generos.Add(generos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generos);
        }

        // GET: Generos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Generos generos = db.Generos.Find(id);
            if (generos == null)
            {
                return HttpNotFound();
            }
            return View(generos);
        }

        // POST: Generos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenerosID,GenerosNombre")] Generos generos)
        {

            var OtroGenero = false;

            generos.GenerosNombre = generos.GenerosNombre.ToLower();

            var mismoNombreGenero = (from a in db.Generos select a).ToList();
            foreach (var item in mismoNombreGenero)
            {
                if (item.GenerosNombre == generos.GenerosNombre)
                {
                    OtroGenero = true;


                }

            }

            if (OtroGenero == false)
            {
                if (ModelState.IsValid)
                {
                    db.Generos.Add(generos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

           // if (ModelState.IsValid)
            //{
              //  db.Entry(generos).State = EntityState.Modified;
                //db.SaveChanges();
               // return RedirectToAction("Index");
            //}
            return View(generos);
        }

        // GET: Generos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Generos generos = db.Generos.Find(id);
        //    if (generos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(generos);
        //}

        // POST: Generos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var mensajeDevuelto = "";
            var libros = (from a in db.Libros where a.GenerosID == id select a).Count();
            if (libros > 0)
            {
                mensajeDevuelto = "No se puede eliminar el Genero selecionado debido a que esta relacionado a un Libro";


            }
            else {
                Generos generos = db.Generos.Find(id);
                db.Generos.Remove(generos);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Index", new { MensajeDevuelto = mensajeDevuelto});
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
