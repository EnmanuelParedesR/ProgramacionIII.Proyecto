using Helper;
using MiPrimerProyectoMVC.Model;
using Model;
using Model.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Model.Commons
{
    public class FrontUser
    {
        public static bool TienePermiso(RolesPermisos valor)
        {
            var a = Convert.ToInt32(valor);
            var usuario = FrontUser.Get();
            return !usuario.Rol.Where(x => x.PermisoID == a)
                               .Any();
        }

        public static Usuario Get()
        {
            return new Usuario().Obtener(SessionHelper.GetUser());
        }
    }
}