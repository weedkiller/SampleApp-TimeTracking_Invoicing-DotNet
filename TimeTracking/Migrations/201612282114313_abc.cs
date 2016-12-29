namespace TimeTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OAuthTokens", "realmid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OAuthTokens", "realmid", c => c.Int(nullable: false));
        }
    }
}
