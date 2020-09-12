using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace wheresmymovies.migration
{
    internal class List
    {
        public static int ListSchema()
        {
            Console.WriteLine("List local or remote [local/remote]:");
            string line;
            while ((line = Console.ReadLine()) != string.Empty)
            {
                if (line == "remote")
                    return ListRemote();
                if (line == "local")
                    return ListLocal();
                return ListSchema();
            }
            return 0;
        }


        private static int ListLocal()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var migrationFolder = $"{assembly.GetName().Name}.Migrations.";
            var lastMigration = assembly.GetManifestResourceNames().Where(name => name.StartsWith(migrationFolder)).Select(path =>  GetMigrationFromPath(path, migrationFolder)).Max();

            Console.WriteLine("Assembly Schema Version {0}", lastMigration);

            return 0;
        }

        private static int GetMigrationFromPath(string path, string folder)
        {
            var filename = path.Substring(folder.Length - 1);
            var expression = new Regex("[0-9]+", RegexOptions.IgnorePatternWhitespace);
            if (!expression.IsMatch(filename)) return 0;
            
            var digit = expression.Match(filename).Value;

            return Convert.ToInt32(digit); 
        }

        private static int ListRemote()
        {
            Console.WriteLine("Enter the connection string:");
            string line;
            while ((line = Console.ReadLine()) != string.Empty)
            {
                try
                {
                    using (var conn = new SqlConnection(line))
                    {
                        conn.Open();
                        var command = conn.CreateCommand();
                        command.CommandText = "SELECT MAX([Version]) FROM [Schema]";

                        var version = command.ExecuteScalar();

                        Console.WriteLine("Database Schema Version: {0}", version);
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return 1;
                }
            }
            return 0;
        }
    }
}
