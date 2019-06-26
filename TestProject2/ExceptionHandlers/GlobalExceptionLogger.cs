using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace TestProject2.ExceptionHandlers
{

    /// <summary>
    /// Global unhandled exception logging/analytics template
    /// </summary>
    /// <remarks>
    /// To register one or more loggers:
    /// <code>
    /// config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLoggerTemplate());
    /// </code>
    /// </remarks>
    public class GlobalExceptionLogger:ExceptionLogger
    {

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            // STEP 1: do whatever analytics you like on the exception
            var ex = context.Exception;

            // example - simple trace logging
            // Trace.WriteLine("*** Exception: " + ex.ToString());

            return Task.FromResult(0);
        }
    }
}