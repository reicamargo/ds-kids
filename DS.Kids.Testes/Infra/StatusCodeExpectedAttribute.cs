using System;
using System.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Infra
{
    public class StatusCodeExpectedAttribute : ExpectedExceptionBaseAttribute
    {

        private readonly HttpStatusCode _httpStatusCode;

        public StatusCodeExpectedAttribute(HttpStatusCode httpStatusCode)
        {
            _httpStatusCode = httpStatusCode;
        }

        protected override void Verify(Exception exception)
        {
            if (exception is UnitTestAssertException)
            {
                RethrowIfAssertException(exception);
            }

            if (exception is AggregateException)
            {
                var innerException = exception.InnerException;
                RethrowIfAssertException(exception);
                Assert.AreEqual(_httpStatusCode, innerException.Data["StatusCode"]);
            }
        }
    }
}
