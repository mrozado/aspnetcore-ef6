namespace EjemploEF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Action = c.Int(nullable: false),
                        GrupoDeAuditoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrupoDeAuditoria", t => t.GrupoDeAuditoriaId, cascadeDelete: true)
                .Index(t => t.GrupoDeAuditoriaId);
            
            CreateTable(
                "dbo.GrupoDeAuditoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Entidad = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auditoria", "GrupoDeAuditoriaId", "dbo.GrupoDeAuditoria");
            DropIndex("dbo.Auditoria", new[] { "GrupoDeAuditoriaId" });
            DropTable("dbo.GrupoDeAuditoria");
            DropTable("dbo.Auditoria");
        }
    }
}
