namespace Model
{
    using Commons;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PermisoDenegadoPorRol")]
    public partial class PermisoDenegadoPorRol
    {
        public PermisoDenegadoPorRol()
        {
            Rol = new List<Rol>();
        }

        public RolesPermisos PermisoID { get; set; }

        public Rol RolID { get; set; }


        public virtual ICollection<Rol> Rol { get; set; }
    }
}
