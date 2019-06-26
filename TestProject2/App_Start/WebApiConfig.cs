
using HandlerTemplates.ExceptionHandlers;
using HandlerTemplates.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using TestProject2.Authentication;
using TestProject2.Authorization;
using TestProject2.CustomAttributesConstraint;
using TestProject2.ExceptionHandlers;
using TestProject2.Filters;
using TestProject2.Handlers;
using RFC7807ErrorMessages;




namespace TestProject2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.SuppressHostPrincipal();
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //handlers
            


            config.MessageHandlers.Add(new FullPipeLineTimerHandler());
            config.MessageHandlers.Add(new ApiKeyHeaderHandler());
            config.MessageHandlers.Add(new HttpMethodOverrideHandler());
            config.MessageHandlers.Add(new ForwardedHeadersHandler());


            // config.Filters.Add(new ActionFilterTemplateAttribute());
            config.Filters.Add(new AuthorizeAttribute());
            config.Filters.Add(new BasicAuthFilter());
           // config.Filters.Add(new RouteTimerFilter("Global"));
            config.Filters.Add(new JwtAuthenticationFilterAttribute());
          //  config.Filters.Add(new ExceptionsFilter());

            //config.Filters.Add(new RequireHttpsAttribute());


            //Exception Logging 

            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new RFC7807GlobalExceptionHandler());
            RFC7807Exception.TypeUriAuthority = "https://example.com";


            var constraintResolver = new DefaultInlineConstraintResolver();
            
            constraintResolver.ConstraintMap.Add("enum", typeof(EnumerationConstraint));            
                constraintResolver.ConstraintMap.Add("validAccount", typeof(ValidAccountConstraint));            
            config.MapHttpAttributeRoutes(constraintResolver);
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/auth /{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //  // handler: new (FullPipeLineTimerHandler) which inherits delegating handler and ovverrides a method other than dispose
            //);
        }
    }
}
