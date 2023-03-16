namespace StudioVeterinario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animale",
                c => new
                    {
                        ID_Animale = c.Int(nullable: false, identity: true),
                        DataRegistrazione = c.DateTime(nullable: false, storeType: "date"),
                        Nome = c.String(nullable: false, maxLength: 50),
                        ColoreMantello = c.String(nullable: false, maxLength: 50),
                        DataNascita = c.DateTime(storeType: "date"),
                        Microchip = c.String(maxLength: 50),
                        NumeroMicrochip = c.String(maxLength: 50),
                        NominativoProprietario = c.String(maxLength: 50),
                        Smarrito = c.Boolean(nullable: false),
                        Foto = c.String(maxLength: 50),
                        DataInizioRicovero = c.DateTime(storeType: "date"),
                        Id_TipologiaAnimale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Animale)
                .ForeignKey("dbo.TipologiaAnimale", t => t.Id_TipologiaAnimale)
                .Index(t => t.Id_TipologiaAnimale);
            
            CreateTable(
                "dbo.TipologiaAnimale",
                c => new
                    {
                        ID_TipologiaAnimale = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID_TipologiaAnimale);
            
            CreateTable(
                "dbo.Visita",
                c => new
                    {
                        ID_Visita = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        Descrizione = c.String(),
                        Id_Animale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Visita)
                .ForeignKey("dbo.Animale", t => t.Id_Animale)
                .Index(t => t.Id_Animale);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.Utente",
                c => new
                    {
                        ID_utente = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Pwd = c.String(nullable: false),
                        Ruolo = c.String(),
                    })
                .PrimaryKey(t => t.ID_utente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visita", "Id_Animale", "dbo.Animale");
            DropForeignKey("dbo.Animale", "Id_TipologiaAnimale", "dbo.TipologiaAnimale");
            DropIndex("dbo.Visita", new[] { "Id_Animale" });
            DropIndex("dbo.Animale", new[] { "Id_TipologiaAnimale" });
            DropTable("dbo.Utente");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Visita");
            DropTable("dbo.TipologiaAnimale");
            DropTable("dbo.Animale");
        }
    }
}
