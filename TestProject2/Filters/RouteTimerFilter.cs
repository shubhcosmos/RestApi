﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TestProject2.Filters
{
    // TODO: Decide if your filter should allow multiple instances per controller or
    //       per-method; set AllowMultiple to true if so
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RouteTimerFilter : ActionFilterAttribute
    {



        // TODO: If you need constructor arguments, create properties to hold them
        //       and public constructors that accept them.
        

        public const string Header = "X-API-Action-Timer";
        public const string TimerPropertyName = "RouteTimerFilter_";
        public string TimerName;


        public RouteTimerFilter(string name =null)
        {
            TimerName = name;
        }
        /// <summary>
        /// Executed BEFORE the controller action method is called
        /// </summary>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            // STEP 1:  Any logic you want to do BEFORE the rest of the action filter chain is 
            //          called, and BEFORE the action method itself.

            // STEP 2: Call the rest of the action filter chain

            var name = TimerName;
            if (string.IsNullOrEmpty(name))
                name = actionContext.ActionDescriptor.ActionName;

            actionContext.Request.Properties[TimerPropertyName + name] = Stopwatch.StartNew();

            await base.OnActionExecutingAsync(actionContext, cancellationToken);

            // STEP 3: Any logic you want to do AFTER the other action filters, but BEFORE
            //         the action method itself is called.
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            // STEP 1:  Any logic you want to do BEFORE the rest of the action filter chain is 
            //          called, and AFTER the action method itself.


            var name = TimerName;
            if (string.IsNullOrEmpty(name))
                name = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            var timer = (Stopwatch)actionExecutedContext.Request.Properties[TimerPropertyName + name];
            var time = timer.ElapsedMilliseconds;

            Trace.Write(actionExecutedContext.Request.Method + " -- " + actionExecutedContext.Request.RequestUri +" -- " + 
                " -- " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName + " -- Elapsed Time for " + name + " -- " + time);

            // STEP 2: Call the rest of the action filter chain
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);

            actionExecutedContext.Response.Headers.Add(Header, time + "msec");
            // STEP 3: Any logic you want to do AFTER the other action filters, and AFTER
            //         the action method itself is called.
        }


    }
}