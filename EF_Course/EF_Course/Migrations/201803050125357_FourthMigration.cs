namespace EF_Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubDirecciones", "Direccion_CodigoDireccion", "dbo.Direcciones");
            DropIndex("dbo.SubDirecciones", new[] { "Direccion_CodigoDireccion" });
            RenameColumn(table: "dbo.Direcciones", name: "CodigoDireccion", newName: "Codigo");
            DropPrimaryKey("dbo.Direcciones");
            AddColumn("dbo.SubDirecciones", "Direccion_Calle", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Direcciones", "Codigo", c => c.Int(nullable: false));
            AlterColumn("dbo.Direcciones", "Calle", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Personas", "Nombre", c => c.String(maxLength: 150, fixedLength: true));
            AddPrimaryKey("dbo.Direcciones", new[] { "Codigo", "Calle" });
            CreateIndex("dbo.SubDirecciones", new[] { "Direccion_CodigoDireccion", "Direccion_Calle" });
            AddForeignKey("dbo.SubDirecciones", new[] { "Direccion_CodigoDireccion", "Direccion_Calle" }, "dbo.Direcciones", new[] { "Codigo", "Calle" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubDirecciones", new[] { "Direccion_CodigoDireccion", "Direccion_Calle" }, "dbo.Direcciones");
            DropIndex("dbo.SubDirecciones", new[] { "Direccion_CodigoDireccion", "Direccion_Calle" });
            DropPrimaryKey("dbo.Direcciones");
            AlterColumn("dbo.Personas", "Nombre", c => c.String(maxLength: 120));
            AlterColumn("dbo.Direcciones", "Calle", c => c.String(maxLength: 300));
            AlterColumn("dbo.Direcciones", "Codigo", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.SubDirecciones", "Direccion_Calle");
            AddPrimaryKey("dbo.Direcciones", "CodigoDireccion");
            RenameColumn(table: "dbo.Direcciones", name: "Codigo", newName: "CodigoDireccion");
            CreateIndex("dbo.SubDirecciones", "Direccion_CodigoDireccion");
            AddForeignKey("dbo.SubDirecciones", "Direccion_CodigoDireccion", "dbo.Direcciones", "CodigoDireccion", cascadeDelete: true);
        }
    }
}
