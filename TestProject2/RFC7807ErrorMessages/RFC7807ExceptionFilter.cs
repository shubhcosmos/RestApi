﻿using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;


namespace RFC7807ErrorMessages
{
    /// <summary>
    /// Web API 2.x exception filter to output valid RFC7807 data to the caller for an exception
    /// </summary>
    public class RFC7807ExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Required method for exception filters
        /// </summary>
        public override Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            var ex = context.Exception as RFC7807Exception;
            if (ex == null)
                ex = new RFC7807Exception(context.Exception, context.Request.RequestUri);

            context.Response = context.Request.CreateRFC7807ProblemResponse(ex.ProblemDetail);

            return Task.FromResult(0);
        }
    }
}