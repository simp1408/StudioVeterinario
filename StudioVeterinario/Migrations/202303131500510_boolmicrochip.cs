namespace StudioVeterinario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolmicrochip : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animale", "Microchip", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animale", "Microchip", c => c.String(maxLength: 50));
        }
    }
}
