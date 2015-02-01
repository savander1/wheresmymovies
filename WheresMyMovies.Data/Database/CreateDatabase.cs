using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheresMyMovies.Data.Database
{
    [TimestampedMigration(2015, 02, 01, 14, 03)]
    public class CreateDatabase : Migration
    {
        public override void Down()
        {
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Username").AsString().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Token").AsString().NotNullable()
                .WithColumn("Role").AsString().NotNullable();
        }
    }
}
