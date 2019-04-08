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
    public class VacacionesController : Controller
    {
        private recurosDBEntities db = new recurosDBEntities();

        // GET: Vacaciones
        public ActionResult Index(String searchString)
        {
            var hola = (from k in db.Vacaciones
                        select k).Include(l => l.Empleados);
            if (!String.IsNullOrEmpty(searchString))
            {
                hola = hola.Where(k => k.Empleado.Contains(searchString));
            }

            return View(hola.ToList());







            //var vacaciones = db.Vacaciones.Include(v => v.Empleados);
            //return View(vacaciones.ToList());
        }

        // GET: Vacaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacaciones vacaciones = db.Vacaciones.Find(id);
            if (vacaciones == null)
            {
                return HttpNotFound();
            }
            return View(vacaciones);
        }

        // GET: Vacaciones/Create
        public ActionResult Create()
        {
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre");
            return View();
        }

        // POST: Vacaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CodigoEmpleado,Empleado,FechaInicial,FechaFinal,Comentario,Año")] Vacaciones vacaciones)
        {
            if (ModelState.IsValid)
            {
                db.Vacaciones.Add(vacaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", vacaciones.CodigoEmpleado);
            return View(vacaciones);
        }

        // GET: Vacaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacaciones vacaciones = db.Vacaciones.Find(id);
            if (vacaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", vacaciones.CodigoEmpleado);
            return View(vacaciones);
        }

        // POST: Vacaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CodigoEmpleado,Empleado,FechaInicial,FechaFinal,Comentario,Año")] Vacaciones vacaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", vacaciones.CodigoEmpleado);
            return View(vacaciones);
        }

        // GET: Vacaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacaciones vacaciones = db.Vacaciones.Find(id);
            if (vacaciones == null)
            {
                return HttpNotFound();
            }
            return View(vacaciones);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacaciones vacaciones = db.Vacaciones.Find(id);
            db.Vacaciones.Remove(vacaciones);
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
