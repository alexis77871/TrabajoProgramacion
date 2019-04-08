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
    public class SalidasController : Controller
    {
        private recurosDBEntities db = new recurosDBEntities();

       


        public ActionResult Activo()
        {
            

            var query = (from a in db.Empleados
                         where a.Estatus == "Activo"
                         select a).Include(e => e.Cargos).Include(k => k.Departamentos);

            return View(query.ToList());
        }

        public ActionResult Inactivo()
        {


            var query = (from a in db.Empleados
                         where a.Estatus == "Inactivo"
                         select a).Include(e => e.Cargos).Include(k => k.Departamentos);

            return View(query.ToList());
        }

        // GET: Salidas
        public ActionResult Index(String searchString)
        {
            var hola =( from k in db.Salidas
                       select k).Include(s => s.Empleados);
            if (!String.IsNullOrEmpty(searchString))
            {
                hola = hola.Where(k => k.Empleado.Contains(searchString));
            }

            return View(hola.ToList());



            //  var salidas = db.Salidas.Include(s => s.Empleados);
            //return View(salidas.ToList());
        }

        // GET: Salidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas salidas = db.Salidas.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            return View(salidas);
        }

        // GET: Salidas/Create
        public ActionResult Create()
        {
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre");
            return View();
        }

        // POST: Salidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CodigoEmpleado,TipoSalida,Empleado,Motivo,FechaSalida")] Salidas salidas)
        {
            var query = (from a in db.Empleados
                         where a.CodigoEmpleado == salidas.CodigoEmpleado
                         select a).First();

            query.Estatus = "Inactivo";
            db.SaveChanges();

            if (ModelState.IsValid)
            {
                db.Salidas.Add(salidas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", salidas.CodigoEmpleado);
            return View(salidas);
        }

        // GET: Salidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas salidas = db.Salidas.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", salidas.CodigoEmpleado);
            return View(salidas);
        }

        // POST: Salidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CodigoEmpleado,TipoSalida,Empleado,Motivo,FechaSalida")] Salidas salidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salidas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoEmpleado = new SelectList(db.Empleados, "CodigoEmpleado", "Nombre", salidas.CodigoEmpleado);
            return View(salidas);
        }

        // GET: Salidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas salidas = db.Salidas.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            return View(salidas);
        }

        // POST: Salidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salidas salidas = db.Salidas.Find(id);
            db.Salidas.Remove(salidas);
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
