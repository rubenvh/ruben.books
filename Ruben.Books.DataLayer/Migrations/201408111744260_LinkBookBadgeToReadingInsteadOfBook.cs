namespace Ruben.Books.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkBookBadgeToReadingInsteadOfBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookBadges", "BookId", "dbo.Books");
            DropIndex("dbo.BookBadges", new[] { "BookId" });
            AddColumn("dbo.BookBadges", "ReadingId", c => c.Int(nullable: false));
            CreateIndex("dbo.BookBadges", "ReadingId");
            AddForeignKey("dbo.BookBadges", "ReadingId", "dbo.Readings", "Id", cascadeDelete: true);
            DropColumn("dbo.BookBadges", "BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookBadges", "BookId", c => c.Int(nullable: false));
            DropForeignKey("dbo.BookBadges", "ReadingId", "dbo.Readings");
            DropIndex("dbo.BookBadges", new[] { "ReadingId" });
            DropColumn("dbo.BookBadges", "ReadingId");
            CreateIndex("dbo.BookBadges", "BookId");
            AddForeignKey("dbo.BookBadges", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
