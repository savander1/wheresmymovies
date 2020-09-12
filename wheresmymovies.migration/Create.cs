using Microsoft.Data.Sqlite;
using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;

namespace wheresmymovies.migration
{
    internal class Create
    {
        internal static int CreateDb()
        {
            string commandText;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = assembly.GetManifestResourceNames().Single(x => x.Contains("CreateDatabase.sql"));
            using (var stream = assembly.GetManifestResourceStream(resource))
            using (var reader = new StreamReader(stream))
            {
                commandText = reader.ReadToEnd();
            }


            Console.WriteLine("Enter the connection string:");
            string line;
            while ((line = Console.ReadLine()) != string.Empty)
            {
                
            }
            return 0;
        }
    }
}