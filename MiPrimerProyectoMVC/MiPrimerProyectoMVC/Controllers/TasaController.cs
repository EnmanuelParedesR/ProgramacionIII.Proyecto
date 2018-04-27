using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerProyectoMVC.Controllers
{
    public class TasaController : Controller
    {
        static string _address = "http://99.92.201.237/tasa5/api/TasasDeIntercambios/?userKey=Team1";
        private string result;

        private async Task<List<Class2>> View()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            JArray resultArray = JArray.Parse(result);

            List<Class2> Data = new List<Class2>();

            int b = 0;
            foreach (var a in resultArray)
            {
                
                var parsed = a.ToObject<Class2>();

                Data[b].ClientKey = parsed.ClientKey;
                Data[b].Fecha = parsed.Fecha;
                Data[b].TasaID = parsed.TasaID;
                Data[b].ValorIntercambio = parsed.ValorIntercambio;
                b++;
            }
            

            return(Data.ToList());
        }


        public class Class2
        {
            public int TasaID { get; set;}
            public int ClientKey  { get; set; }
            public double ValorIntercambio { get; set; }
            public DateTime Fecha { get; set; }
        }


    }
}