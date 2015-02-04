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
        }
    }
}
Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Year);
            Map(x => x.Rating);
            Map(x => x.Released);

            HasManyToMany<Talent>(x => x.Director);
            HasManyToMany<Talent>(x => x.Writer);
            HasManyToMany<Talent>(x => x.Actor);

            Map(x => x.Plot);
            Map(x => x.Language);
            Map(x => x.Country);
            Map(x => x.Poster);
            Map(x => x.MovieType);