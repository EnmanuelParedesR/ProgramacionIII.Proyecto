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
using System.Net.Http;
using System.Collections;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MiPrimerProyectoMVC.Model
{

    [AutenticadoAttribute]
    public class ServicesController : Controller
    {

        private Proyecto_FinalEntities db = new Proyecto_FinalEntities();
        static string _address = "http://99.92.201.237/servicio6/Api/Servicios/";
        private string result;

        public JsonConverter[] JsonDataType { get; private set; }

        public async Task<ActionResult> Get()
        {
            var result = await GetExternalResponse();

            return View();
        }

        private async Task<string> GetExternalResponse()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            JArray resultArray = JArray.Parse(result);
            Service Servicio = new Service();

            foreach (var a in resultArray)
            {
                var parsed = a.ToObject<Class1>();

                Servicio = db.Services.FirstOrDefault(x=> x.ExternalID == parsed.IDServicio || x.ServiceName == parsed.Nombre);

                if(Servicio == null)
                {
                    Servicio = new Service();
                    Servicio.ServiceName = parsed.Nombre;
                    Servicio.Precio = Convert.ToInt32(parsed.Precio);
                    Servicio.ExternalID = parsed.IDServicio;

                    db.Services.Add(Servicio);
                    db.SaveChanges();
                }    
            }
            return result;
        }



        public ActionResult Index()
        {
           
            return View(db.Services.ToList());
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Detalles_Servicioss))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Servicios))]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Crear_Servicios))]
        public async Task<ActionResult> Create([Bind(Include = "ServiceID,ServiceName,ServiceDescription,IsActive,IsUsed,Precio,ExternalID")] Service service)
        {
            try
            {
                if (!(service.ServiceName != null))
                {
                    throw new VoidServiceNameException("Se requiere llenar el campo correspondiente al nombre del servicio");
                }

                if (ModelState.IsValid)
                {
                    db.Services.Add(service);

                    Class1 Post_data = new Class1()
                    {
                        Nombre = service.ServiceName,
                        Precio = Convert.ToInt32(service.Precio)
                    };
               
                    var client = new HttpClient();
                    HttpResponseMessage response = await client.PostAsJsonAsync(_address,Post_data);
                    response.EnsureSuccessStatusCode();

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (VoidServiceNameException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
                ViewBag.VoidServiceName = "Se requiere llenar el campo correspondiente al nombre del servicio";
            }

            return View(service);
        }

        // GET: Services/Edit/5
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Servicios))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Editar_Servicios))]
        public ActionResult Edit([Bind(Include = "ServiceID,ServiceName,ServiceDescription,IsActive,IsUsed,Precio,ExternalID")] Service service)
        {
            try
            {
                if (!(service.ServiceName != null))
                {
                    throw new VoidServiceNameException("Se requiere llenar el campo correspondiente al nombre del servicio");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(service).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (VoidServiceNameException)
            {
                ViewBag.ErrorMessage = "Faltan campos por llenar";
                ViewBag.VoidServiceName = "Se requiere llenar el campo correspondiente al nombre del servicio";
            }

            return View(service);
        }

        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Servicios))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermisoAttribute(Permiso = (RolesPermisos.Puede_Borrar_Servicios))]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            try
            {
                foreach (var a in db.Plan_Service)
                {
                    if (a.ServiceID == service.ServiceID)
                    {
                        db.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                    }
                }


                if(service.ExternalID != null && service.ServiceDescription != null)
                {
                    Class2 Post_Data = new Class2()
                    {
                        IDServicio = service.ExternalID
                    };

                    var client = new HttpClient();
                    HttpResponseMessage response = await client.DeleteAsync(String.Concat(_address, Post_Data.IDServicio));  //_address.Concat(Post_Data.IDServicio));
                    response.EnsureSuccessStatusCode();

                }

                db.Services.Remove(service);
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



    public class RootObject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public int IDServicio { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }

    public class Class2
    {
        public int? IDServicio { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }

}
