using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace wheresmymovies.test
{
    [TestClass]
    public abstract class BaseTest
    {
        public TestContext TestContext { get; set; }
        public string Class =>  TestContext.FullyQualifiedTestClassName.Replace("wheresmymovies.test.", string.Empty);
        public string Method => TestContext.TestName;

        private static readonly TraceListener Listener = new TextWriterTraceListener(Console.OpenStandardOutput(), "wheresmymoviesListener");

        public BaseTest()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            if (!Trace.Listeners.Contains(Listener))
                Trace.Listeners.Add(Listener);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Trace.WriteLine($"({TestContext.CurrentTestOutcome}) - {Class}.{Method}");
            Trace.Flush();
            
        }
    }
}
