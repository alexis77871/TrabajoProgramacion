using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoRH.Models;

namespace ProyectoRH.Controllers
{
    public class DepartamentosController : Controller
    {
        private recurosDBEntities db = new recurosDBEntities();

        // GET: Departamentos
        public ActionResult Index(String searchString)
        {
            var hola = (from k in db.Departamentos
                        select k);
            if (!String.IsNullOrEmpty(searchString))
            {
                hola = hola.Where(k => k.Nombre.Contains(searchString));
            }

            return View(hola.ToList());



            //return View(db.Departamentos.ToList());
        }

        // GET: Departamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamentos departamentos = db.Departamentos.Find(id);
            if (departamentos == null)
            {
                return HttpNotFound();
            }
            return View(departamentos);
        }

        // GET: Departamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoDepartamento,Nombre")] Departamentos departamentos)
        {
            if (ModelState.IsValid)
            {
                db.Departamentos.Add(departamentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departamentos);
        }

        // GET: Departamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamentos departamentos = db.Departamentos.Find(id);
            if (departamentos == null)
            {
                return HttpNotFound();
            }
            return View(departamentos);
        }

        // POST: Departamentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoDepartamento,Nombre")] Departamentos departamentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamentos);
        }

        // GET: Departamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamentos departamentos = db.Departamentos.Find(id);
            if (departamentos == null)
            {
                return HttpNotFound();
            }
            return View(departamentos);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamentos departamentos = db.Departamentos.Find(id);
            db.Departamentos.Remove(departamentos);
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
