using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TestProject2.Handlers
{
    public class FullPipeLineTimerHandler : DelegatingHandler
    {
        const string _header = "X-API-Timer";
        
        protected override async   Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // STEP 1: Global message-level logic that must be executed BEFORE the request
            //          is sent on to the action method


            // STEP 2: Call the rest of the pipeline, all the way to a response message

            var timer = Stopwatch.StartNew();


            var response = await base.SendAsync(request, cancellationToken);



            // STEP 3: Any global message-level logic that must be executed AFTER the request
            //          has executed, before the final HTTP response message

            var elapsed = timer.ElapsedMilliseconds;

            Trace.Write("PipeLine+Action time in milliseconds = " + elapsed);

            response.Headers.Add(_header, elapsed + "msec");
            // STEP 4:  Return the final HTTP response
            return response;

        }

    }
}