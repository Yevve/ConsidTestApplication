namespace ConsidTestApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LibraryItem",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Title = c.String(),
                        Author = c.String(),
                        Pages = c.Int(),
                        RunTimeMinutes = c.Int(),
                        IsBorrowable = c.Boolean(nullable: false),
                        Borrower = c.String(),
                        BorrowDate = c.DateTime(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCEO = c.Boolean(nullable: false),
                        IsManager = c.Boolean(nullable: false),
                        ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsCEO, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LibraryItem", "CategoryID", "dbo.Category");
            DropIndex("dbo.Employees", new[] { "IsCEO" });
            DropIndex("dbo.LibraryItem", new[] { "CategoryID" });
            DropTable("dbo.Employees");
            DropTable("dbo.LibraryItem");
            DropTable("dbo.Category");
        }
    }
}
