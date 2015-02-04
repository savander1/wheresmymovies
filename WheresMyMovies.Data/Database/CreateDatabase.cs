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

            Create.Table("Movie")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Year").AsDate().NotNullable()
                .WithColumn("Rating").AsString().NotNullable()
                .WithColumn("Plot").AsString(15000).NotNullable()
                .WithColumn("Language").AsString().Nullable()
                .WithColumn("Country").AsString().Nullable()
                .WithColumn("Poster").AsString().Nullable()
                .WithColumn("MovieType").AsString().NotNullable();

            Create.Table("Genre")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Category").AsString().NotNullable();

            Create.Table("MovieGenre")
                .WithColumn("MovieId").AsInt32().ForeignKey().NotNullable()
                .WithColumn("GenreId").AsInt32().ForeignKey().NotNullable();

            Create.Table("Talent")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("FirstName").AsString().Nullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Type").AsString().NotNullable();

            Create.Table("MovieTalent")
                .WithColumn("MovieId").AsInt32().ForeignKey().NotNullable()
                .WithColumn("TalentId").AsInt32().ForeignKey().NotNullable();
        }
    }
}