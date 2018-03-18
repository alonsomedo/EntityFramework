namespace EF_Course.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Direcciones", "Calle", c => c.String(maxLength: 300));
            AlterColumn("dbo.Personas", "Nombre", c => c.String(maxLength: 120));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "Nombre", c => c.String());
            AlterColumn("dbo.Direcciones", "Calle", c => c.String());
        }
    }
}
