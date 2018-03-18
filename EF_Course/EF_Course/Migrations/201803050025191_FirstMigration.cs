namespace EF_Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubDirecciones",
                c => new
                    {
                        SubDireccionId = c.Int(nullable: false, identity: true),
                        SubCalle = c.String(maxLength: 124),
                        Direccion_CodigoDireccion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubDireccionId)
                .ForeignKey("dbo.Direcciones", t => t.Direccion_CodigoDireccion, cascadeDelete: true)
                .Index(t => t.Direccion_CodigoDireccion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubDirecciones", "Direccion_CodigoDireccion", "dbo.Direcciones");
            DropIndex("dbo.SubDirecciones", new[] { "Direccion_CodigoDireccion" });
            DropTable("dbo.SubDirecciones");
        }
    }
}
