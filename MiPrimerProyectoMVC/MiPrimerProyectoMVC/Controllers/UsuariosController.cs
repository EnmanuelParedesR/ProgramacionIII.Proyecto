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
using Model.Commons;
using MiPrimerProyectoMVC.Tags;
using MiPrimerProyectoMVC.Controllers;

namespace MiPrimerProyectoMVC.Controllers
{
    [AutenticadoAttribute]
    public class UsuariosController : Controller
    {
        private DataBase db = new DataBase();

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Lista_Usuario))]
        public ActionResult Index()
        {

            var usuario = db.Usuario.Include(u => u.Rol);
            return View(usuario.ToList());
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Detalles_Usuario))]
         public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Usuario))]
        public ActionResult Create()
        {
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Usuario))]
        public ActionResult Create([Bind(Include = "id,Nombre,Correo,Password,Rol_id")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Usuario))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Usuario))]
        public ActionResult Edit([Bind(Include = "id,Nombre,Correo,Password,Rol_id")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }


        // GET: Usuarios/Edit/5
        public ActionResult Informacion()
        {
            if (FrontUser.Get().id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(FrontUser.Get().id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Informacion([Bind(Include = "id,Nombre,Correo,Password,Rol_id")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home", new {  });

             
            }
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }




        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Usuario))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Usuario))]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
