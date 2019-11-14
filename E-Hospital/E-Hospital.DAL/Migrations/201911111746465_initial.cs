namespace E_Hospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SpecializationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.SpecializationId);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        RoleId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MedicalCard = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        Comment = c.String(),
                        VisitTime = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitRequests", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.VisitRequests", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "Id", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Patients", "Id", "dbo.Users");
            DropForeignKey("dbo.Doctors", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.VisitRequests", new[] { "PatientId" });
            DropIndex("dbo.VisitRequests", new[] { "DoctorId" });
            DropIndex("dbo.Patients", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Doctors", new[] { "SpecializationId" });
            DropIndex("dbo.Doctors", new[] { "Id" });
            DropTable("dbo.VisitRequests");
            DropTable("dbo.Roles");
            DropTable("dbo.Patients");
            DropTable("dbo.Users");
            DropTable("dbo.Specializations");
            DropTable("dbo.Doctors");
        }
    }
}
