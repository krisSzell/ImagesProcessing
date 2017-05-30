namespace ImagesProcessing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPhotoModelWithByteArray : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "PhData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "PhData");
        }
    }
}
