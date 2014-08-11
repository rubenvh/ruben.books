namespace Ruben.Books.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnershipOfBooksAndBookBadgesConcept : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Owned", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Owned");
        }
    }
}
