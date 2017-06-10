using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace wheresmymovies.test
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExpectedExceptionMessage : ExpectedExceptionBaseAttribute
    {
        private readonly string _message;
        private readonly Type _type;

        public ExpectedExceptionMessage(Type type, string message)
        {
            _message = message;
            _type = type;
        }

        protected override void Verify(Exception exception)
        {
            var message = exception.Message.Trim();
            Assert.IsTrue(!string.IsNullOrEmpty(message) && message.Contains(_message), 
                $"Exception.Message expected to contain {_message} but found {message}.");
        }
    }
}
