namespace GridFilteringMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeChanges : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tblEmployee", new[] { "PerformanceManager_ID" });
            AlterColumn("dbo.tblEmployee", "PerformanceManager_ID", c => c.Int());
            CreateIndex("dbo.tblEmployee", "PerformanceManager_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tblEmployee", new[] { "PerformanceManager_ID" });
            AlterColumn("dbo.tblEmployee", "PerformanceManager_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.tblEmployee", "PerformanceManager_ID");
        }
    }
}
