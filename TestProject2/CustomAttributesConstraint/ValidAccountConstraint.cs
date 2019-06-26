using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace TestProject2.CustomAttributesConstraint
{
    public class ValidAccountConstraint : IHttpRouteConstraint
    {
        private bool IsValidAccount(string sAccount)
        {
            return (!String.IsNullOrEmpty(sAccount) &&
                sAccount.StartsWith("shubh") && sAccount.Length >=5);

        }
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {

                return IsValidAccount(value as string);

            }
            return false;
        }
    }
}
