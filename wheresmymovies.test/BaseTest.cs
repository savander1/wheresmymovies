using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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
            if (!Trace.Listeners.Contains(Listener))
                Trace.Listeners.Add(Listener);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Trace.WriteLine($"({TestContext.CurrentTestOutcome}) - {Class}.{Method}");
            Trace.Flush();
        }

        public static async Task CheckExeceptionMessageAsync(Func<Task> action, string message)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(ex.Message) && ex.Message.Contains(message),
                $"Exception.Message expected to contain {message} but found {ex.Message}.");
                throw ex;
            }
        }

        public static async Task CheckExeceptionMessageAsync(Action action, string message)
        {
            try
            {
                await Task.Run(action);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(ex.Message) && ex.Message.Contains(message),
                $"Exception.Message expected to contain {message} but found {ex.Message}.");
                throw ex;
            }
        }
    }
}
