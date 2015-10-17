using System;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApplication2
{
    public class DataGenerator
    {
        private readonly IoPath _paths;
        private readonly OMovieDatabaseReader _reader;

        public DataGenerator(IoPath paths)
        {
            _paths = paths;
            _reader = new OMovieDatabaseReader();
        }

        public void GenerateJson()
        {
            using (var stream = File.Open(_paths.InFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    var line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        var data = _reader.GetData(line);
                        if (data.Contains("\"Response\":\"False\""))
                        {
                            data = line + " " + data;
                        }
                        using (
                            var writeStream = File.Open(_paths.OutFilePath, FileMode.Append, FileAccess.Write,
                                                        FileShare.Write))
                        {
                            using (var writer = new StreamWriter(writeStream))
                            {
                                writer.WriteLine(data);
                            }
                        }

                        line = reader.ReadLine();
                    }
                }
            }
        }
    }
}