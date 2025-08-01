﻿namespace Proje4_EntityFrameworkCodeFirstMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovieDurationToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Duration", c => c.String());
        }
    }
}
