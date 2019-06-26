using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TestProject2.Authorization
{
    /// <summary>
    /// Authorization happens AFTER authentication. By the time your authorization filter
    /// is called the authenticated identity should be set (if credentials were provided).
    /// </summary>
    // TODO: Decide if your filter should allow multiple instances per controller or
    //       per-method; set AllowMultiple to false if not
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequiresClaimAttribute : AuthorizationFilterAttribute
    {
        // TODO: If you need constructor arguments, create properties to hold them
        //       and public constructors that accept them.

            /// <summary>
            /// Constructor comma delimited claim types
            /// </summary>
            /// <param name="sList"> slist =  comma seperatewd claim types</param>
        public RequiresClaimAttribute(string sList)
        {
            if (sList != null)

            {
                ClaimTypes = sList.Split(',').
                    Where(x => !string.IsNullOrEmpty(x))
                    .ToArray();

            }
        }

        /// <summary>
        /// List of supported token schemes
        /// </summary>
        public readonly string[] ClaimTypes = null;

        /// <summary>
        /// If true full list of missing claims is included in response 
        /// </summary>
        public bool IncludeMissingInresponse { get; set; }
        /// <summary>
        /// Called when authorization must be checked; 
        /// </summary>
        public override  Task OnAuthorizationAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            // STEP 1: Perform your authorization logic
            // The authentication filters should have set an IPrincipal for you 
            // with various properties
          
            var authorized = true; // DoSomeAuthorizationLogicMaybeEvenAsync
            var principal = actionContext.RequestContext.Principal;

            if (principal == null || principal.Identity == null ||  !principal.Identity.IsAuthenticated)
            {
                authorized = false;
            }

            

            //...though it is possible to have an authorization filter without or 
            // independent of authentication; perhaps based the presence of certain 
            // http headers in the request.  In that case use the appropriate logic. 

            // You can cast the IPrincipal to a specific class type to access the 
            // claims or properties of the authenticated principal:
            //var specificIdentityType = principal.Identity as ClaimsIdentity;
            //var claim = specificIdentityType.Claims.FirstOrDefault(a => a.Type.Equals("MyClaim"));

            

            // STEP 2: If authorization fails, set the HTTP reponse and exit
            if (!authorized)
            {
                // Which code to return is a bit religious. https://stackoverflow.com/questions/3297048/403-forbidden-vs-401-unauthorized-http-responses
                // But I prefer either 403 Forbidden or 404 Not Found for authorization issues 
                // (404 if for security reasons you want to disguise the fact that it was 
                // an authorization issue, to avoid giving an attacker too much information).
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)//{ Content = ...};
                {
                    ReasonPhrase = "Unauthorized"
                };

                return Task.FromResult(0);
            }

            if (! (principal.Identity is ClaimsIdentity))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)//{ Content = ...};
                {
                    ReasonPhrase = "Incorrect token "
                };

                return Task.FromResult(0);

            }


                       if (ClaimTypes == null ||ClaimTypes.Length ==0)
             return base.OnAuthorizationAsync(actionContext, cancellationToken);


            var ClaimsId = principal.Identity as ClaimsIdentity;
                
            var missing = new List<string>(); 


            foreach (var item in ClaimTypes)
            {

                if (!ClaimsId.HasClaim(a => a.Type.Equals(item, StringComparison.InvariantCultureIgnoreCase)))
                {
                    
                    missing.Add(item);

                }
            }


            if (missing.Count>0)
            {

                if (IncludeMissingInresponse)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, missing);

                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                    actionContext.Response.ReasonPhrase = "Required claims missing";

                }

                return Task.FromResult(0);


            }

            return base.OnAuthorizationAsync(actionContext, cancellationToken); 
        }

    }
}