using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TestProject2.Filters
{
    /// <summary>
    /// Enum for type of client side caching
    /// </summary>
    /// <remarks> See this article for details of each:
    /// https://developers.google.com/web/fundamentals/performance/optimizing-content-efficiency/http-caching
    /// </remarks>
    
           public enum ClientCacheControl
    {
        Public,     // can be cached by intermediate devices even if authentication was used;
        Private,    // browser-only, no intermediate caching, typically for per-user data
        NoCache     // no caching by browser or intermediate devices
    };

    
    // TODO: Decide if your filter should allow multiple instances per controller or
    //       per-method; set AllowMultiple to true if so
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method , AllowMultiple =false)]
    public class ClientCacheControlFilter : ActionFilterAttribute
    {


        public ClientCacheControl CacheType;
        public double ChacheSeconds;

        // TODO: If you need constructor arguments, create properties to hold them
        //       and public constructors that accept them.
        public ClientCacheControlFilter(double seconds =60.0)
        {
            ChacheSeconds = seconds;
            CacheType = ClientCacheControl.Private; 
           

        }

        public ClientCacheControlFilter( ClientCacheControl cacheType  ,double seconds = 60.0)
        {
            ChacheSeconds = seconds;
            CacheType = cacheType;

            if (cacheType == ClientCacheControl.NoCache)
                ChacheSeconds = -1;


        }

        /// <summary>
        /// Executed BEFORE the controller action method is called
        /// </summary>
        public override async Task  OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            // STEP 1:  Any logic you want to do BEFORE the rest of the action filter chain is 
            //          called, and BEFORE the action method itself.

            // STEP 2: Call the rest of the action filter chain
             await   base.OnActionExecutingAsync(actionContext, cancellationToken);

            // STEP 3: Any logic you want to do AFTER the other action filters, but BEFORE
            //         the action method itself is called.
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            // STEP 1:  Any logic you want to do BEFORE the rest of the action filter chain is 
            //          called, and AFTER the action method itself.

            // STEP 2: Call the rest of the action filter chain
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);

            // STEP 3: Any logic you want to do AFTER the other action filters, and AFTER
            //         the action method itself is called.


            if (CacheType == ClientCacheControl.NoCache)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoStore = true

                };

                // for older browser
                actionExecutedContext.Response.Headers.Pragma.TryParseAdd("no-cache");

                // create a date if not present so we can have expires match it

                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                    actionExecutedContext.Response.Headers.Date = DateTimeOffset.UtcNow;

                if (actionExecutedContext.Response.Content != null)
                    actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date;

            }

            else
            {

                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {

                    Public = (CacheType == ClientCacheControl.Public),
                    Private = (CacheType == ClientCacheControl.Private),
                    NoCache = false,
                    MaxAge = TimeSpan.FromSeconds(ChacheSeconds)


                };



                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                    actionExecutedContext.Response.Headers.Date = DateTimeOffset.UtcNow;

                if (actionExecutedContext.Response.Content != null)
                    actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date.Value.AddSeconds(ChacheSeconds); ;


            }
        }







    }
}