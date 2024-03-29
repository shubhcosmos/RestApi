﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace RFC7807ErrorMessages
{
    /// <summary>
    /// Global exception handler to convert all exceptions into RFC7807 messages as the response
    /// </summary>
    /// <remarks>
    /// To register, in WebApiConfig.cs:
    /// <code>
    /// config.Services.Replace(typeof(IExceptionHandler), new RFC7807GlobalExceptionHandler());
    /// </code>
    /// </remarks>
    public class RFC7807GlobalExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// Required method for a web api global exception handler
        /// </summary>
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // The stock web api global handler throws an exception in this case;
            // duplicating that logic.
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // verify this exception should be handled at all; exit if not
            if (!ShouldHandle(context))
            {
                return Task.FromResult(0);
            }

            // convert the exception into an RFC7807Exception if required, then create an IHttpActionResult from it
            var ex = context.Exception as RFC7807Exception;
            if (ex == null)
                ex = new RFC7807Exception(context.Exception, context.Request.RequestUri);

            context.Result = context.Request.CreateRFC7807ProblemActionResult(ex.ProblemDetail);

            return Task.FromResult(0);
        }

    }
}
