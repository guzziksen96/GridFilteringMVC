namespace GridFilteringMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblDepartment",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.tblEmployee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                        Grade = c.Int(),
                        DepartmentID = c.Int(nullable: false),
                        PerformanceManager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tblDepartment", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.tblEmployee", t => t.PerformanceManager_ID)
                .Index(t => t.DepartmentID)
                .Index(t => t.PerformanceManager_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblEmployee", "PerformanceManager_ID", "dbo.tblEmployee");
            DropForeignKey("dbo.tblEmployee", "DepartmentID", "dbo.tblDepartment");
            DropIndex("dbo.tblEmployee", new[] { "PerformanceManager_ID" });
            DropIndex("dbo.tblEmployee", new[] { "DepartmentID" });
            DropTable("dbo.tblEmployee");
            DropTable("dbo.tblDepartment");
        }
    }
}
