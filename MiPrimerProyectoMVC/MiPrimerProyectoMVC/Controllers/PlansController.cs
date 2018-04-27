using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC.Model;
using MiPrimerProyectoMVC.Controllers;
using MiPrimerProyectoMVC.Tags;
using Model.Commons;

namespace MiPrimerProyectoMVC.Model

{
    [AutenticadoAttribute]
    public class PlansController : Controller
    {
        private Proyecto_FinalEntities db = new Proyecto_FinalEntities();

        
       
        public ActionResult Index()
        {
            foreach (var a in db.Plans )
            {
                foreach(var b  in db.Services)
                {
                    if (a.PlanID == b.ServiceID)
                    {
               
                        a.Costo += b.Precio;
                        a.Price = Convert.ToDouble(a.Costo + 100);

                        db.SaveChanges();
                    }
                }
            }

            return View(db.Plans.ToList());           
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Detalles_Planes))]
        public ActionResult Details(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);  
            if (plan == null)
            {
                return HttpNotFound();
            }

            return View(plan);
        }

        // GET: Plans/Create
         [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Planes))]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Planes))]
        public ActionResult Create([Bind(Include = "PlanID,PlanName,PlanDescription,Price,IsActive,IsUsed,Costo")] Plan plan)
        {
            bool error = false;

            try
            {
                if (!(plan.PlanName != null))
                {
                    ViewBag.VoidPlanName = "Se requiere llenar el campo correspondiente al nombre del plan";
                }
                if (error)
                {
                    throw new VoidValueException();
                }

                if (ModelState.IsValid)
                {
                    db.Plans.Add(plan);
                    db.SaveChanges();
                    Utilidades utl = new Utilidades();

                    if (!utl.IsRentable(plan.PlanID,Convert.ToDecimal(plan.Price)))
                    {
                        return RedirectToAction("Edit", "Plan_Service", new { area = "" });
                    }

                    foreach(var a in db.Plan_Service)
                    {
                        if (plan.PlanID == a.PlanID)
                            utl.ActualizarCosto(plan.PlanID, a.ServiceID);
                    }
                 

                   
                 
                    return RedirectToAction("Create", "Plan_Service", new { area = "" });
                }
            }
            catch (VoidValueException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
            }

            return View(plan);
        }

        // GET: Plans/Edit/5
         [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Planes))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Planes))]
        public ActionResult Edit([Bind(Include = "PlanID,PlanName,PlanDescription,Price,IsActive,IsUsed,Costo")] Plan plan)
        {
            bool error = false;
            try
            {
                if (!(plan.PlanName != null))
                {
                    ViewBag.VoidPlanName = "Se requiere llenar el campo correspondiente al nombre del plan";
                }
                if (error)
                {
                    throw new VoidValueException();
                }

                if (ModelState.IsValid)
                {
                    db.Entry(plan).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (VoidValueException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
            }

            return View(plan);
        }

        // GET: Plans/Delete/5
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Planes))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // POST: Plans/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Planes))]
        public ActionResult DeleteConfirmed(int id)
        {
            Plan plan = db.Plans.Find(id);
            plan.IsUsed = false;
            try
            {
                if (plan.IsUsed)
                {
                    throw new CurrentlyInUseException();
                }
                
                foreach(var a in db.Plan_Service)
                {
                    if(a.PlanID == plan.PlanID)
                    {
                        db.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var a in db.Subscriptions)
                {
                    if (a.PlanID == plan.PlanID)
                    {
                        db.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                db.Plans.Remove(plan);
                db.SaveChanges();
            }
            catch (CurrentlyInUseException)
            {
                ViewBag.ErrorMessage = "El campo que desea eliminar esta siendo usado actualmente, y no puede ser borrado hasta entonces";
            }

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
