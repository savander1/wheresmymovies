﻿using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace wheresmymovies.migration
{
    internal class Create
    {
        internal static int CreateDb()
        {
            string commandText;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CreateDatabase.sql"))
            using (var reader = new StreamReader(stream))
            {
                commandText = reader.ReadToEnd();
            }

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
                        command.CommandText = commandText;

                        command.ExecuteNonQuery();
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