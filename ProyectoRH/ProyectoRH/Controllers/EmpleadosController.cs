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
    public class EmpleadosController : Controller
    {
        private recurosDBEntities db = new recurosDBEntities();



        // GET: Empleados
        public ActionResult Index(String searchString)
        {
            var hola = (from k in db.Empleados
                        select k).Include(e => e.Cargos).Include(e => e.Departamentos);
            if (!String.IsNullOrEmpty(searchString))
            {
                hola = hola.Where(k => k.Nombre.Contains(searchString));
            }

            return View(hola.ToList());


            // var empleados = db.Empleados.Include(e => e.Cargos).Include(e => e.Departamentos);
            //return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.CodigoCargo = new SelectList(db.Cargos, "CodigoCargo", "Cargo");
            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "CodigoDepartamento", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoEmpleado,Nombre,Apellido,Telefono,Departamento,Cargo,FechaIngreso,Salario,CodigoDepartamento,CodigoCargo,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoCargo = new SelectList(db.Cargos, "CodigoCargo", "Cargo", empleados.CodigoCargo);
            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "CodigoDepartamento", "Nombre", empleados.CodigoDepartamento);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoCargo = new SelectList(db.Cargos, "CodigoCargo", "Cargo", empleados.CodigoCargo);
            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "CodigoDepartamento", "Nombre", empleados.CodigoDepartamento);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoEmpleado,Nombre,Apellido,Telefono,Departamento,Cargo,FechaIngreso,Salario,CodigoDepartamento,CodigoCargo,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoCargo = new SelectList(db.Cargos, "CodigoCargo", "Cargo", empleados.CodigoCargo);
            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "CodigoDepartamento", "Nombre", empleados.CodigoDepartamento);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
