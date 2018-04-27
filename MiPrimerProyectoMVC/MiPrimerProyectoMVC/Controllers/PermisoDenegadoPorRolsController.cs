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
    public class PermisoDenegadoPorRolsController : Controller
    {
        private DataBaseEntities1 db = new DataBaseEntities1();
        private DataBase db1 = new DataBase();


        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Rol))]
        public ActionResult Intermediaro()
        {
            return View();
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Rol))]
        public ActionResult Intermediaro_Error()
        {
            return View();
        }


        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Lista_Rol))]
        public ActionResult Index()
        {
            var Nombres_Permiso = Nombre_Permiso(); // Se obtienen los nombres de los permisos
            var Nombres_Rol = Nombre_Rol(); // Se obtienen los nombres de los roles

            ViewBag.Nombres_Permiso = Nombres_Permiso;
            ViewBag.Nombres_Rol = Nombres_Rol;
            return View(db.PermisoDenegadoPorRols.ToList());
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Rol))]
        public ActionResult Create()
        {
            var Nombres_Permiso = db1.Permiso.ToList();
            ViewBag.Nombres_Permiso = Nombres_Permiso;

            ViewBag.RolID = new SelectList(db1.Rol, "id", "Nombre");
            return View();
        }
       
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Rol))]
        public ActionResult Create([Bind(Include = "ID,PermisoID,RolID")] PermisoDenegadoPorRol permisoDenegadoPorRol)
        {
            if (ModelState.IsValid)
            {
                if (!db.PermisoDenegadoPorRols.Any(x => x.PermisoID == permisoDenegadoPorRol.PermisoID && x.RolID == permisoDenegadoPorRol.RolID))
                {
                    db.PermisoDenegadoPorRols.Add(permisoDenegadoPorRol);
                    db.SaveChanges();
                    return RedirectToAction("Intermediaro");
                }   
            }

            var Nombres_Permiso = db1.Permiso.ToList();
            ViewBag.Nombres_Permiso = Nombres_Permiso;
            ViewBag.RolID = new SelectList(db1.Rol, "id", "Nombre",permisoDenegadoPorRol.RolID);
            return RedirectToAction("Intermediaro_Error");
        }



        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Rol))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermisoDenegadoPorRol permisoDenegadoPorRol = db.PermisoDenegadoPorRols.FirstOrDefault(x => x.ID == id);
            if (permisoDenegadoPorRol == null)
            {
                return HttpNotFound();
            }

            var Nombres_Permiso = Nombre_Permiso(); // Se obtienen los nombres de los permisos
            var Nombres_Rol = Nombre_Rol(); // Se obtienen los nombres de los roles

            ViewBag.Nombres_Permiso = Nombres_Permiso;
            ViewBag.Nombres_Rol = Nombres_Rol;
            return View(permisoDenegadoPorRol);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Rol))]
        public ActionResult DeleteConfirmed(int id)
        {
            PermisoDenegadoPorRol permisoDenegadoPorRol = db.PermisoDenegadoPorRols.FirstOrDefault(x => x.ID == id);
            db.PermisoDenegadoPorRols.Remove(permisoDenegadoPorRol);
            db.SaveChanges();


            var Nombres_Permiso = Nombre_Permiso(); // Se obtienen los nombres de los permisos
            var Nombres_Rol = Nombre_Rol(); // Se obtienen los nombres de los roles


            ViewBag.Nombres_Permiso = Nombres_Permiso;
            ViewBag.Nombres_Rol = Nombres_Rol;
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


        public Dictionary<int, string> Nombre_Permiso()
        {
            Dictionary<int, string> Nombres_Permiso = new Dictionary<int, string>();

            foreach (var a in db1.Permiso)
            {
               foreach (var b in db.PermisoDenegadoPorRols)
                {
                    if (Convert.ToInt32(a.PermisoID) == b.PermisoID)
                    {
                        if (!Nombres_Permiso.ContainsKey(b.PermisoID))
                        {
                            Nombres_Permiso.Add(b.PermisoID, a.Descripcion);
                        }
                    }
                }
         }
            return Nombres_Permiso;
        }

        public Dictionary<int, string> Nombre_Rol()
        {
            Dictionary<int, string> Nombres_Rol = new Dictionary<int, string>();

            foreach (var a in db1.Rol)
            {

                foreach (var b in db.PermisoDenegadoPorRols)
                {
                    if (Convert.ToInt32(a.id) == b.RolID)
                    {

                        if (!Nombres_Rol.ContainsKey(b.RolID))
                        {
                            Nombres_Rol.Add(b.RolID, a.Nombre);
                        }

                    }
                }

            }
            return Nombres_Rol;
        }

    }
}
