using System.Linq;

namespace ConsoleApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any()) return;
            if (args.Count() != 2) return;

            var inFilePath = args[0];
            var outFilePath = args[1];

            var paths = new IoPath(inFilePath, outFilePath);
            paths.Validate();

            var dataGenerator = new DataGenerator(paths);
            dataGenerator.GenerateJson();
        }
    }
}
