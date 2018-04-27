namespace MiPrimerProyectoMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlumno : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adjunto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Alumno_id = c.Int(nullable: false),
                        Archivo = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Alumno", t => t.Alumno_id, cascadeDelete: true)
                .Index(t => t.Alumno_id);
            
            CreateTable(
                "dbo.Alumno",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellido = c.String(nullable: false, maxLength: 100),
                        Sexo = c.Int(nullable: false),
                        FechaNacimiento = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AlumnoCurso",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Alumno_id = c.Int(nullable: false),
                        Curso_id = c.Int(nullable: false),
                        Nota = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Curso", t => t.Curso_id, cascadeDelete: true)
                .ForeignKey("dbo.Alumno", t => t.Alumno_id, cascadeDelete: true)
                .Index(t => t.Alumno_id)
                .Index(t => t.Curso_id);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Permiso",
                c => new
                    {
                        PermisoID = c.Int(nullable: false),
                        Modulo = c.String(nullable: false, maxLength: 20, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.PermisoID);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, unicode: false),
                        Correo = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        Rol_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Rol", t => t.Rol_id)
                .Index(t => t.Rol_id);
            
            CreateTable(
                "dbo.PermisoDenegadoPorRol",
                c => new
                    {
                        PermisoID = c.Int(nullable: false),
                        RolID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermisoID, t.RolID })
                .ForeignKey("dbo.Permiso", t => t.PermisoID, cascadeDelete: true)
                .ForeignKey("dbo.Rol", t => t.RolID, cascadeDelete: true)
                .Index(t => t.PermisoID)
                .Index(t => t.RolID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermisoDenegadoPorRol", "RolID", "dbo.Rol");
            DropForeignKey("dbo.PermisoDenegadoPorRol", "PermisoID", "dbo.Permiso");
            DropForeignKey("dbo.Usuario", "Rol_id", "dbo.Rol");
            DropForeignKey("dbo.AlumnoCurso", "Alumno_id", "dbo.Alumno");
            DropForeignKey("dbo.AlumnoCurso", "Curso_id", "dbo.Curso");
            DropForeignKey("dbo.Adjunto", "Alumno_id", "dbo.Alumno");
            DropIndex("dbo.PermisoDenegadoPorRol", new[] { "RolID" });
            DropIndex("dbo.PermisoDenegadoPorRol", new[] { "PermisoID" });
            DropIndex("dbo.Usuario", new[] { "Rol_id" });
            DropIndex("dbo.AlumnoCurso", new[] { "Curso_id" });
            DropIndex("dbo.AlumnoCurso", new[] { "Alumno_id" });
            DropIndex("dbo.Adjunto", new[] { "Alumno_id" });
            DropTable("dbo.PermisoDenegadoPorRol");
            DropTable("dbo.Usuario");
            DropTable("dbo.Rol");
            DropTable("dbo.Permiso");
            DropTable("dbo.Curso");
            DropTable("dbo.AlumnoCurso");
            DropTable("dbo.Alumno");
            DropTable("dbo.Adjunto");
        }
    }
}
