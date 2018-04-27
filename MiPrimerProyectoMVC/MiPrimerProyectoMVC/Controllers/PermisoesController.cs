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
using MiPrimerProyectoMVC.Controllers;
using MiPrimerProyectoMVC.Tags;


namespace MiPrimerProyectoMVC.Controllers
{
    [AutenticadoAttribute]
    public class PermisoesController : Controller
    {
        private DataBase db = new DataBase();

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Lista_Rol))]
        public ActionResult Index()
        {
            return View(db.Permiso.ToList());
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
