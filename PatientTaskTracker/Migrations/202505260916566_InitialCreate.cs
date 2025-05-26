namespace PatientTaskTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItems", "PatientId", "dbo.Patients");
            DropIndex("dbo.TaskItems", new[] { "PatientId" });
            DropTable("dbo.TaskItems");
            DropTable("dbo.Patients");
        }
    }
}
