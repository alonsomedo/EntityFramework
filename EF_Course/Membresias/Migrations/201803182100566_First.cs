namespace Membresias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LugarNacimiento", c => c.String(maxLength: 120));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LugarNacimiento");
        }
    }
}
