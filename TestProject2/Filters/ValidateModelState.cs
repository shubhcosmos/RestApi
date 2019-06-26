using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace TestProject2.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateModelState:ActionFilterAttribute
    {

        // TODO: If you need constructor arguments, create properties to hold them
        //       and public constructors that accept them.
        public ValidateModelState()
        { }

        public bool Bodyrequired { get; set; }

        /// <summary>
        /// Executed BEFORE the controller action method is called
        /// </summary>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            // STEP 1:  Any logic you want to do BEFORE the rest of the action filter chain is 
            //          called, and BEFORE the action method itself.


            if (!actionContext.ModelState.IsValid)
            {

                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }

            else if (Bodyrequired)

            {

                foreach (var item in actionContext.ActionDescriptor.ActionBinding.ParameterBindings)
                {

                    if (item.WillReadBody) {

                        if (!actionContext.ActionArguments.ContainsKey(item.Descriptor.ParameterName) || 
                            actionContext.ActionArguments[item.Descriptor.ParameterName] == null)


                        {

                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, item.Descriptor.ParameterName);


                        }

                        break;
                    }


                }



            }
            // STEP 2: Call the rest of the action filter chain
            await base.OnActionExecutingAsync(actionContext, cancellationToken);

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
        }


    }
}