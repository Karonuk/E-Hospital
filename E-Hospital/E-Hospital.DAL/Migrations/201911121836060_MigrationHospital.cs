namespace E_Hospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationHospital : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VisitRequests", "IsApproved", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VisitRequests", "IsApproved", c => c.Boolean(nullable: false));
        }
    }
}
