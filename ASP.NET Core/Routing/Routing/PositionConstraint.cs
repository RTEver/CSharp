using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Routing
{
    public class PositionConstraint : Object, IRouteConstraint
    {
        private readonly String[] positions = new[] { "admin", "director", "accountant" };

        public Boolean Match(HttpContext httpContext, IRouter route, String routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            httpContext.Items["Info"] = values[routeKey];

            return positions.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
}
