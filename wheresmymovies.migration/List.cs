using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

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
            var lastMigration = assembly.GetManifestResourceNames().Where(name => name.StartsWith(migrationFolder)).Select(path => /* get real migration number*/ 1).Max();

            Console.WriteLine("Assembly Schema Version {0}", lastMigration);

            return 0;

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
