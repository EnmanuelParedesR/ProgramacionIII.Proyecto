using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC.Model;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiPrimerProyectoMVC.Controllers
{
    public class TasaIDsController : Controller
    {
        private Proyecto_FinalEntities db = new Proyecto_FinalEntities();
        static string _address = "http://99.92.201.237/tasa5/api/TasasDeIntercambios/?userKey=Team1";
        private string result;


        // GET: TasaIDs
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            JArray resultArray = JArray.Parse(result);

            List<TasaID> Tasa = new List<TasaID>();

            foreach (var a in resultArray)
            {
                var parsed = a.ToObject<TasaID>();

                Tasa.Add(parsed);
            }

            bool areEqual = true;
            foreach (var a in Tasa)
                {
                foreach (var b in db.TasaIDs)
                {
                    if (b.ClientKey != a.ClientKey && b.Fecha == a.Fecha)
                    {
                       areEqual = true;

                    }
                    else
                    {
                        areEqual = false;
                    }
                }

                if (areEqual)
                {
                    db.TasaIDs.Add(a);
                }
            }         
            db.SaveChanges();
            return View(db.TasaIDs.ToList());
        }

        // GET: TasaIDs/Create
        public ActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientKey,ValorIntercambio,Fecha")] TasaID tasaID)
        {
            if (ModelState.IsValid)
            {
                db.TasaIDs.Add(tasaID);

                //Class1 Post_data = new Class1();

                //Post_data.ClientKey = tasaID.ClientKey;
                //Post_data.Fecha = tasaID.Fecha;
                //Post_data.ValorIntercambio = Convert.ToDouble(tasaID.ValorIntercambio);

                //var client1 = new HttpClient();
                //HttpResponseMessage response = await client1.PostAsJsonAsync(_address, Post_data);
                //response.EnsureSuccessStatusCode();


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tasaID);
        }

        // GET: TasaIDs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasaID tasaID = db.TasaIDs.Find(id);
            if (tasaID == null)
            {
                return HttpNotFound();
            }
            return View(tasaID);
        }

        // POST: TasaIDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TasaID tasaID = db.TasaIDs.Find(id);
            db.TasaIDs.Remove(tasaID);
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

    public class Class1
    {
       
        public string ClientKey { get; set; }
        public double ValorIntercambio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
