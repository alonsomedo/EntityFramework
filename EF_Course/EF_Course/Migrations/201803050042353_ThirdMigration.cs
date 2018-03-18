namespace EF_Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "Sexo", c => c.Int(nullable: false));
            AddColumn("dbo.SubDirecciones", "Numero", c => c.Int(nullable: false));
            CreateIndex("dbo.Personas", new[] { "Edad", "Nacimiento" }, name: "Ix_Nacimiento_Edad");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Personas", "Ix_Nacimiento_Edad");
            DropColumn("dbo.SubDirecciones", "Numero");
            DropColumn("dbo.Personas", "Sexo");
        }
    }
}
