using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC;
using Model;
using MiPrimerProyectoMVC.Tags;
using Model.Commons;
using MiPrimerProyectoMVC.Model;

namespace MiPrimerProyectoMVC.Controllers
{
    [AutenticadoAttribute]
    public class RolsController : Controller
    {
        private DataBase db = new DataBase();
        private DataBaseEntities1 db1 = new DataBaseEntities1();
        // GET: Rols

        [PermisoAttribute(Permiso =(RolesPermisos.Puede_Lista_Rol))]
        public ActionResult Index()
        {
            return View(db.Rol.ToList());
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Detalles_Rol))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }

            PermisoDenegadoPorRolsController b = new PermisoDenegadoPorRolsController();

            var a = db1.PermisoDenegadoPorRols.Where(x => x.RolID == id).ToList();
           

            ViewBag.Nombre_Permiso = b.Nombre_Permiso();
            ViewBag.PDR = a;

            return View(rol);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Rol))]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Rol))]
        public ActionResult Create([Bind(Include = "id,Nombre")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Rol.Add(rol);
                db.SaveChanges();
                return RedirectToAction("Create", "PermisoDenegadoPorRols", new { area = "" });
            }

            return View(rol);
        }

       [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Rol))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }


            PermisoDenegadoPorRolsController b = new PermisoDenegadoPorRolsController();

            var a = db1.PermisoDenegadoPorRols.Where(x => x.RolID == id).ToList();


            ViewBag.Nombre_Permiso = b.Nombre_Permiso();
            ViewBag.PDR = a;


            return View(rol);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Rol))]
        public ActionResult Edit([Bind(Include = "id,Nombre")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Rol))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }


            PermisoDenegadoPorRolsController b = new PermisoDenegadoPorRolsController();

            var a = db1.PermisoDenegadoPorRols.Where(x => x.RolID == id).ToList();


            ViewBag.Nombre_Permiso = b.Nombre_Permiso();
            ViewBag.PDR = a;

            return View(rol);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Rol))]
        public ActionResult DeleteConfirmed(int id)
        {
            Rol rol = db.Rol.Find(id);

            foreach (var a in db.Usuario)
            {
                if (a.Rol_id == rol.id)
                {
                    db.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            if(rol.id != 3)
            {
                db.Rol.Remove(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
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
