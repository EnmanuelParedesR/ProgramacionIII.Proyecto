using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC.Model;
using MiPrimerProyectoMVC.Tags;
using Model.Commons;

namespace MiPrimerProyectoMVC.Controllers
{
    [AutenticadoAttribute]
    public class Plan_ServiceController : Controller
    {
        private Proyecto_FinalEntities db = new Proyecto_FinalEntities();



        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Planes))]
        public ActionResult Intermediaro()
        {
            return View();
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Detalles_Planes))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan_Service plan_Service = db.Plan_Service.Find(id);
            if (plan_Service == null)
            {
                return HttpNotFound();
            }
            return View(plan_Service);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Planes))]
        public ActionResult Intermediaro_Error()
        {
            return View();
        }


        // GET: Plan_Service/Create
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Planes))]
        public ActionResult Create()
        {
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName");
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Planes))]
        public ActionResult Create([Bind(Include = "Plan_Service1,PlanID,ServiceID")] Plan_Service plan_Service)
        {
            if (ModelState.IsValid)
            {
                if(!db.Plan_Service.Any(x => x.PlanID == plan_Service.PlanID && x.ServiceID == plan_Service.ServiceID))
                {
                    db.Plan_Service.Add(plan_Service);
                    db.SaveChanges();
                    return RedirectToAction("Intermediaro");
                }
            
            }

            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName", plan_Service.PlanID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceName", plan_Service.ServiceID);
            return RedirectToAction("Intermediaro_Error");
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Planes))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan_Service plan_Service = db.Plan_Service.Find(id);
            if (plan_Service == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName", plan_Service.PlanID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceName", plan_Service.ServiceID);
            return View(plan_Service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Planes))]
        public ActionResult Edit([Bind(Include = "Plan_Service1,PlanID,ServiceID")] Plan_Service plan_Service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plan_Service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Plans", new { id = plan_Service.PlanID });
            }
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName", plan_Service.PlanID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceName", plan_Service.ServiceID);
            return RedirectToAction("Details", "Plans", new { id = plan_Service.PlanID });

        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Planes))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan_Service plan_Service = db.Plan_Service.Find(id);
            if (plan_Service == null)
            {
                return HttpNotFound();
            }
            return View(plan_Service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Planes))]
        public ActionResult DeleteConfirmed(int id)
        {
            Plan_Service plan_Service = db.Plan_Service.Find(id);
            db.Plan_Service.Remove(plan_Service);
            db.SaveChanges();
            return RedirectToAction("Details", "Plans", new { id = plan_Service.PlanID });
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
