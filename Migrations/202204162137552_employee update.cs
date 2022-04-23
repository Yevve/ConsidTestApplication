namespace ConsidTestApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeupdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "IsCEO" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Employees", "IsCEO", unique: true);
        }
    }
}
