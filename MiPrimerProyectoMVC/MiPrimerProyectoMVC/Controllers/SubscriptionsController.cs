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


namespace MiPrimerProyectoMVC.Model
{   
    [AutenticadoAttribute]
    public class SubscriptionsController : Controller
    {
    
        private Proyecto_FinalEntities db = new Proyecto_FinalEntities();

        
        public ActionResult Index()
        {
            var subscriptions = db.Subscriptions.Include(s => s.Client).Include(s => s.Plan);

       
                return View(db.Subscriptions.ToList());


        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Detalles_Subscriptions))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Subscriptions))]
         public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FirstName");
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName");
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Subscriptions))]
        public ActionResult Create([Bind(Include = "SubscriptionID,PlanID,ClientID,ExpirationDate,IsExpired,IsUsed")] Subscription subscription)
        {
            try
            {
                if (!(subscription.ExpirationDate != null))
                {
                    throw new VoidExpirationDateException("Se requiere llenar el campo correspondiente a la fecha de expiracion de la subscripcion");
                }

                if (ModelState.IsValid)
                {
                    subscription.IsUsed = true;
                    db.Subscriptions.Add(subscription);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (VoidExpirationDateException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
                ViewBag.VoidExpirationDate = "Se requiere llenar el campo correspondiente a la fecha de expiracion de la subscripcion";
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FirstName", subscription.ClientID);
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName", subscription.PlanID);
            return View(subscription);
        }

        // GET: Subscriptions/Edit/5
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Subscriptions))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FirstName", subscription.ClientID);
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName", subscription.PlanID);
            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Subscriptions))]
        public ActionResult Edit([Bind(Include = "SubscriptionID,PlanID,ClientID,ExpirationDate,IsExpired,IsUsed")] Subscription subscription)
        {
            try
            {
                if (!(subscription.ExpirationDate != null))
                {
                    throw new VoidExpirationDateException("Se requiere llenar el campo correspondiente a la fecha de expiracion de la subscripcion");
                }

                if (ModelState.IsValid)
                {
                    db.Plans.Find(subscription.PlanID).IsUsed = true;
                    db.Services.Find(subscription.ClientID).IsUsed = true;
                    db.Entry(subscription).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (VoidExpirationDateException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
                ViewBag.VoidExpirationDate = "Se requiere llenar el campo correspondiente a la fecha de expiracion de la subscripcion";
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FirstName", subscription.ClientID);
            ViewBag.PlanID = new SelectList(db.Plans, "PlanID", "PlanName", subscription.PlanID);
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Subscriptions))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Subscriptions))]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            subscription.IsUsed = false;
            try
            {
                if (subscription.IsUsed)
                {
                    throw new CurrentlyInUseException();
                }
                db.Subscriptions.Remove(subscription);
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
