using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.IO;
using MiPrimerProyectoMVC.Tags;
using Helper;
using Model.Commons;

namespace MiPrimerProyectoMVC.Controllers
{
    [AutenticadoAttribute]
    public class HomeController : Controller
    {
    

        public ActionResult Index()
        {
            MathProxy proxy = new MathProxy();
            double a = 5.5;
            double b = 10.5;
            ViewBag.Value1 = a;
            ViewBag.Value2 = b;
            ViewBag.ProxyMessage = proxy.Add(a, b);
            return View();
        }


        public ActionResult Salir()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }
    }
}