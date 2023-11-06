using System.Globalization;
using System.Text.RegularExpressions;
using static TodaysMonet.Statuses;

namespace TodaysMonet.Constraints
{
    public class NoZeroesRouteConstraint : IRouteConstraint
    {
        private static readonly Regex _regex = new(
            @"^[1-9]*$",
            RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
            TimeSpan.FromMilliseconds(100));

        public bool Match(
            HttpContext? httpContext, IRouter? route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var routeValue))
            {
                return false;
            }

            var routeValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);

            if (routeValueString is null)
            {
                return false;
            }

            return _regex.IsMatch(routeValueString);
        }
    }

    public class StatusesConstraint: IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            object? routeValue;
            if (!values.TryGetValue(routeKey, out routeValue))
            {
                return false;
            }
            else if (!Enum.IsDefined(typeof(Statuses), routeValue.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
