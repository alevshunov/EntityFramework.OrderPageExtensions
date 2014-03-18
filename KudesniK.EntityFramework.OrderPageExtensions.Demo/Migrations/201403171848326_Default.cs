using System.Data.Entity.Migrations;

namespace KudesniK.EntityFramework.OrderPageExtensions.Demo.Migrations
{
    public partial class Default : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Population = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Percent = c.Single(nullable: false),
                        Source = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Countries");
        }
    }
}
