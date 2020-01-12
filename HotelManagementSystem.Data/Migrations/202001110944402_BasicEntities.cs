namespace HotelManagementSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAccomodationPackage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationTypeID = c.Int(nullable: false),
                        Name = c.String(),
                        NoOfRooms = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tblAccomodationType", t => t.AccomodationTypeID, cascadeDelete: true)
                .Index(t => t.AccomodationTypeID);
            
            CreateTable(
                "dbo.tblAccomodationType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tblAccomodation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tblAccomodationPackage", t => t.AccomodationPackageID, cascadeDelete: true)
                .Index(t => t.AccomodationPackageID);
            
            CreateTable(
                "dbo.tblBooking",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationID = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblAccomodation", "AccomodationPackageID", "dbo.tblAccomodationPackage");
            DropForeignKey("dbo.tblAccomodationPackage", "AccomodationTypeID", "dbo.tblAccomodationType");
            DropIndex("dbo.tblAccomodation", new[] { "AccomodationPackageID" });
            DropIndex("dbo.tblAccomodationPackage", new[] { "AccomodationTypeID" });
            DropTable("dbo.tblBooking");
            DropTable("dbo.tblAccomodation");
            DropTable("dbo.tblAccomodationType");
            DropTable("dbo.tblAccomodationPackage");
        }
    }
}
