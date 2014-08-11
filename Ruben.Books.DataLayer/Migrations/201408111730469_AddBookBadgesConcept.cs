namespace Ruben.Books.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookBadgesConcept : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookBadges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        IsSpent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookBadges", "BookId", "dbo.Books");
            DropIndex("dbo.BookBadges", new[] { "BookId" });
            DropTable("dbo.BookBadges");
        }
    }
}
