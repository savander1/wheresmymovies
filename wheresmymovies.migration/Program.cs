using Microsoft.Extensions.CommandLineUtils;
using System;

namespace wheresmymovies.migration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "Database Initializer and Migrator";
            app.Description = "Initializes or migrates the Where's My Movies? application database";

            app.HelpOption("-?|-h|--help");

            var createOption = app.Option("-c|--create", "Creates a new database", CommandOptionType.NoValue);
            var populateOption = app.Option("-p|--populate", "Populates the database wtih test data", CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                if (createOption.HasValue())
                {
                    Console.WriteLine("Creating Database");
                    return Create.CreateDb();
                }

                if (populateOption.HasValue())
                {
                    Console.WriteLine("Populating Database");
                    return Populate.CreateDb();
                }

                
                app.ShowHint();
                return 0;
            });

            app.Command("migrate", (command) =>
            {
                command.Description = "This is the description for simple-command.";
                command.HelpOption("-?|-h|--help");

                var versionOption = command.Option("-v| --version<value>", "Specify the version of the database schema required. \r\n For the latest version, use the value \"latest\"", CommandOptionType.SingleValue);
                var listOption = command.Option("-l| --list", "List the schema of the database or the migrator", CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    if (versionOption.HasValue())
                    {
                        Console.WriteLine("Migrating to version {0}", versionOption.Value());
                    }
                    else if (listOption.HasValue())
                    {
                        Console.WriteLine("Executing list program");
                        return List.ListSchema();
                    }
                    else
                    {
                        command.ShowHint();
                    }
                    return 0;
                });
            });

            app.Execute(args);
        }
    }
}
