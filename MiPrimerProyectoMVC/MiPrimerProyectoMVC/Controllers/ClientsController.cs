using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC.Model;
using System.ComponentModel.DataAnnotations;
using MiPrimerProyectoMVC.Tags;
using Model.Commons;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MiPrimerProyectoMVC.Controllers
{       
    [AutenticadoAttribute]
    public class ClientsController : Controller
    {
        private Proyecto_FinalEntities db = new Proyecto_FinalEntities();
        static string _address = "http://99.92.201.237/tasa5/api/TasasDeIntercambios/?userKey=Team1";
        private string result;

        public ViewResult Index()
        {
     
            return View(db.Clients.ToList());
                
        }

        public async Task<ViewResult> Create_Cliente(string RNC)
        {

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address + RNC);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            JArray resultArray = JArray.Parse(result);


            return View();
        }



        [PermisoAttribute (Permiso = (RolesPermisos.Puede_Detalles_Cliente))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        
        // GET: Clients/Create
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Cliente))]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Cliente))]
        public ActionResult Create([Bind(Include = "ClientID,FirstName,MiddleName,LastName,PassportNumber,IsActive,IsUsed")] [Required] Client client)
        {
            bool error = false;
            try
            {
                if (!(client.FirstName != null))
                {
                    ViewBag.VoidFirstName = "Se requiere llenar el campo correspondiente al primer nombre";
                    error = true;
                }
                if (!(client.LastName != null))
                {
                    ViewBag.VoidLastName = "Se requiere llenar el campo correspondiente a los apellidos";
                    error = true;
                }
                if (error)
                {
                    throw new VoidValueException();
                }

                if (ModelState.IsValid)
                {

               

                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (VoidValueException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
            }


            return View(client);
        }



        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Cliente))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

       
            

            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Cliente))]
        public ActionResult Edit([Bind(Include = "ClientID,FirstName,MiddleName,LastName,PassportNumber,IsActive,IsUsed")] Client client)
        {
            bool error = false;
            try
            {
                if (!(client.FirstName != null))
                {
                    ViewBag.VoidFirstName = "Se requiere llenar el campo correspondiente al primer nombre";
                    error = true;
                }
                if (!(client.LastName != null))
                {
                    ViewBag.VoidLastName = "Se requiere llenar el campo correspondiente a los apellidos";
                    error = true;
                }
                if (error)
                {
                    throw new VoidValueException();
                }

                if (ModelState.IsValid)
                {

                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (VoidValueException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
            }

            return View(client);
        }

        // GET: Clients/Delete/5
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Cliente))]
         public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Cliente))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Client client = db.Clients.Find(id);
            foreach (var a in db.Subscriptions)
            {
                if (client.ClientID == a.ClientID)
                {
                    db.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                }
            }
            db.Clients.Remove(client);
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


        public class RNC12
        {

            [JsonProperty("ID")]
            public int ID { get; set; }

            [JsonProperty("RNC")]
            public string RNC { get; set; }

            [JsonProperty("RazonSocial")]
            public string RazonSocial { get; set; }


        }

    

}
