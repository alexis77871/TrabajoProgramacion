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
    public class NominasController : Controller
    {
        private recurosDBEntities db = new recurosDBEntities();
        
        

        public ActionResult Totalizar()
        {
            var query = (from a in db.Empleados
                         where a.Estatus == "Activo"
                         select a);
            ViewBag.TotalSalario = query.Sum(a => a.Salario);
            ViewBag.TotalEmpleados = query.Count();

            


            db.SaveChanges();
            return View();
        }
        // GET: Nominas
        public ActionResult Index(string searchString)
        {
            var hola = from k in db.Nomina
                       select k;
            if (!String.IsNullOrEmpty(searchString))
            {
                hola = hola.Where(k => k.Mes.Contains(searchString));

            }
           
            return View(hola.ToList());

        }

        // GET: Nominas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // GET: Nominas/Create
        public ActionResult Create()
        {
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre");
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre");
            return View();
        }

        // POST: Nominas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Empleado,Año,Mes,MontoTotal,CodigoEmpleado")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Nomina.Add(nomina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", nomina.CodigoEmpleado);
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", nomina.CodigoEmpleado);
            return View(nomina);
        }

        // GET: Nominas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", nomina.CodigoEmpleado);
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", nomina.CodigoEmpleado);
            return View(nomina);
        }

        // POST: Nominas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Empleado,Año,Mes,MontoTotal,CodigoEmpleado")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", nomina.CodigoEmpleado);
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", nomina.CodigoEmpleado);
            return View(nomina);
        }

        // GET: Nominas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nomina.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // POST: Nominas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nomina nomina = db.Nomina.Find(id);
            db.Nomina.Remove(nomina);
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
