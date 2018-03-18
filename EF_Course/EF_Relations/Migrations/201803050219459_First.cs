namespace EF_Relations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class First : DbMigration
    {
        public override void Up()
        {


            CreateTable(
                "dbo.Cursoes",
                c => new
                {
                    CursoId = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(),
                })
                .PrimaryKey(t => t.CursoId);

            CreateTable(
                "dbo.Persona_Curso",
                c => new
                {
                    PersonaId = c.String(nullable: false, maxLength: 128),
                    CursoId = c.Int(nullable: false),
                    Abandonado = c.Boolean(nullable: false),
                    Persona_Cedula = c.String(maxLength: 128),
                })
                .PrimaryKey(t => new { t.PersonaId, t.CursoId })
                .ForeignKey("dbo.Cursoes", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.Persona_Cedula)
                .Index(t => t.CursoId)
                .Index(t => t.Persona_Cedula);

            CreateTable(
                "dbo.Personas",
                c => new
                {
                    Cedula = c.String(nullable: false, maxLength: 128),
                    Nombre = c.String(),
                    Nacimiento = c.DateTime(nullable: false),
                    Edad = c.Int(nullable: false),
                    Sexo = c.Int(nullable: false),
                    Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Direccion_CodigoDireccion = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Cedula)
                .ForeignKey("dbo.Direccions", t => t.Direccion_CodigoDireccion)
                .Index(t => t.Direccion_CodigoDireccion);

            CreateTable(
                "dbo.Direccions",
                c => new
                {
                    CodigoDireccion = c.Int(nullable: false, identity: true),
                    Calle = c.String(),
                })
                .PrimaryKey(t => t.CodigoDireccion);

            CreateTable(
                "dbo.TarjetaDeCreditoes",
                c => new
                {
                    TCreditoId = c.Int(nullable: false, identity: true),
                    Numero = c.String(),
                    Persona_Cedula = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.TCreditoId)
                .ForeignKey("dbo.Personas", t => t.Persona_Cedula, cascadeDelete: true)
                .Index(t => t.Persona_Cedula);

            CreateStoredProcedure("dbo.SP_PERSONAS_POR_EDAD", x => new { Edad = x.Int() },
                    @"SELECT NOMBRE,EDAD FROM dbo.PERSONAS
                                            WHERE EDAD = @EDAD"
                    );

            CreateStoredProcedure("dbo.SP_PERSONAS_MAYORES_EDAD", x => new { Edad = x.Int(18) },
                        @"SELECT NOMBRE,EDAD FROM dbo.PERSONAS
                                                WHERE EDAD >= @EDAD"
                );

            Sql(RecursosSQL.C_SP_BORRA_PERSONAS_MENORES);

        }

        public override void Down()
        {
            
            DropForeignKey("dbo.TarjetaDeCreditoes", "Persona_Cedula", "dbo.Personas");
            DropForeignKey("dbo.Persona_Curso", "Persona_Cedula", "dbo.Personas");
            DropForeignKey("dbo.Personas", "Direccion_CodigoDireccion", "dbo.Direccions");
            DropForeignKey("dbo.Persona_Curso", "CursoId", "dbo.Cursoes");
            DropIndex("dbo.TarjetaDeCreditoes", new[] { "Persona_Cedula" });
            DropIndex("dbo.Personas", new[] { "Direccion_CodigoDireccion" });
            DropIndex("dbo.Persona_Curso", new[] { "Persona_Cedula" });
            DropIndex("dbo.Persona_Curso", new[] { "CursoId" });
            DropTable("dbo.TarjetaDeCreditoes");
            DropTable("dbo.Direccions");
            DropTable("dbo.Personas");
            DropTable("dbo.Persona_Curso");
            DropTable("dbo.Cursoes");
            Sql(RecursosSQL.D_SP_BORRA_PERSONAS_MENORES);
            DropStoredProcedure("SP_PERSONAS_MAYORES_EDAD");
            DropStoredProcedure("SP_PERSONAS_POR_EDAD");
        }
    }
}
