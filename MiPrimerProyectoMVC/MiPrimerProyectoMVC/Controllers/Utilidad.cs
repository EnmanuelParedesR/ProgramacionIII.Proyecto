using MiPrimerProyectoMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MiPrimerProyectoMVC
{
    public class Utilidades
    {
        private Proyecto_FinalEntities db = new Proyecto_FinalEntities();

  
        public bool IsRentable(int id, decimal precio)
        {
            Plan plan = db.Plans.Find(id);
            decimal costo = 0;

            if (plan.Costo != null)
            {
               costo = Convert.ToDecimal(plan.Costo);
            }
           
            if (precio > costo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public decimal Costo(int idPlan)
        {
            int? costo = 0;
            foreach (Plan_Service relacion in db.Plan_Service)
            {
                if (relacion.PlanID == idPlan)
                {
                    Service servicio = db.Services.Find(relacion.PlanID);

                    costo = costo + servicio.Precio;
                }
            }
            return Convert.ToDecimal(costo);
        }

        public void ActualizarCosto(int idplan, int idServ)
        {
            Plan plan = db.Plans.Find(idplan);
            decimal? costo = 0;
            foreach (var item in db.Plan_Service)
            {
                if (plan.PlanID == item.PlanID)
                {
                    Service serv = db.Services.Find(item.ServiceID);
                    costo += serv.Precio;
                }
            }
            plan.Costo = costo;
            db.SaveChanges();
        }

  
    }
}

