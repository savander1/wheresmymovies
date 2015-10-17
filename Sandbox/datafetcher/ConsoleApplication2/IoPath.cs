using System;
using System.IO;

namespace ConsoleApplication2
{
    public class IoPath
    {
        public string InFilePath { get; private set; }
        public string OutFilePath { get; private set; }

        public IoPath(string inFilePath, string outFilePath)
        {
            if (inFilePath == null) throw new ArgumentNullException("inFilePath");
            if (outFilePath == null) throw new ArgumentNullException("outFilePath");

            InFilePath = inFilePath;
            OutFilePath = outFilePath;
        }

        public void Validate()
        {
            if (!File.Exists(InFilePath))
                throw new FileNotFoundException("Input file not found", InFilePath);

            if (!File.Exists(OutFilePath))
            {
                using (var stream = File.Create(OutFilePath))
                {
                    stream.Flush();
                }
            }
        }
    }
}